# Notes

## Units en discussies

```cs
public class Calculator
{
    public int Result { get; set; }

    public void Add(int x)
    {
        Result += this.rationalize(x);

        // is het nog een *unit*test als...
        // - er operator overloading plaatsvindt bij het ophogen van Result?
        // - we Math.rationalize() aanroepen, een static methode van .NET?
        // - we this.rationalize() aanroepen, een private methode in deze class? 

        // en was als...
        // - rationalize() roept een database aan? zeker geen unit
        // - rationalize() roept een API aan in Zwitserland - zeker geen unit
        // - rationalize() is een static binnen je eigen class - unit

        // JP zei net meerdere classes is integratie dus uh
    }

    private int static rationalize() {}
}
```

### Willen we private unittesten?

Ja!

- via een soort van public manier
- maar niet rechtstreeks. zo min mogelijk rechtstreeks.
  - via de public API
  - mocht via public API niet lukken of mocht de public API te veel andere units raken, wat dan?
    - Reflection - introspectie 
    - Microsoft.Fakes?
    - `[InternalsVisibleTo("DemoProject.Tests")]`

### Black box vs white box?

- black box ben je publieke API aan het roepen zonder kennis van internals
  - grotere kans dat er iets stukgaat omdat je "meer code" aanroept
- white box heb je wel kennis van internals
  - internals veranderen vaker, dus test valt sneller om door aanpassingen
  - private state is nog steeds vervelend om in te stellen

## xUnit vs NUnit vs MSTest

- MSTest werkte niet bij .NET Core 1.0
- xUnit/NUnit: data-driven tests
- guidance framework: MSTest

```cs
`[TestMethod]` // MSTest
`[Fact]`/`[Theory]` // xUnit
`[Test]` // NUnit
```

## integration test? wat integreer je?

- als je de db bij je tests betrekt
- API-controller test
- alles wat met iets anders integreert
- class A met class B
- frontend: HTML renderen
- hier roep je altijd nog code aan

End-to-end/UI test
- hier roep je geen code aan

## Right BICEP

Acroniem voor "wat kunnen we nog meer testen?"

- Right: are the results correct/right?
- Boundary: `int.MaxValue` `int.MinValue` `''` `null`
- Inverse
- Cross-check using other means: all employees - non-male employees = nr of male employees
- Exceptions/Errors: forceer errorsituaties
- Performance: een sorteeralgoritme mag niet 20s doen over 5 items sorteren

## Test-driven development (TDD)

1. Schrijf een test
2. Run de test en zie dat hij FAALT
3. Schrijf code - implementeer
4. Run de/alle test/tests en zie dat hij SLAAGT
5. Refactor - code aan guidelines laten voldoen

Repeat.

Ook wel: RED-GREEN-REFACTOR

En waarom?
- zekerheid
- elk dingetje hebt getest
- minder snel buiten scope
  - gestructureerder te werk gaan
- Deadlines en dat kwaliteitswaarborging door middel van testen schrijven niet iedere keer het slachtoffer is

## JP's onofficiele lijstje van wanneer je niet hoeft te unittesten

- proof of concept
  - let op: PoC wordt toch ook nog wel eens het uitgangspunt van het project
- als je project < 6 maanden mee gaat
   "niets is zo permanent als een tijdelijke oplossing"
- als de stagiaire alle tests mag schrijven
  - de developer die de functionaliteit maakt, hoort de tests te schrijven
- als je x% code coverage wil halen
- AI-generated tests

wanneer geen TDD?
- als de architectuur te veel onbekende variabelen heeft. Zoek architectuur uit en dan weer terug naar TDD.


## Testdata

Notoir berucht vervelende dingen met testen: testdata. Zeker met 5000+ tests.

- 1 almachtige testset voor zoveel tests is niet tof, want een aanpassing in die testset doet tests breken
- iedere test z'n eigen testdata is veel duplicatie. Een aanpassing in het datamodel betekent een hoop tests corrigeren.
- Ertussenin: per 100 tests een testset?

Patterns willen ook helpen:
- builder
- factory

```cs
var customer = CustomerFactory.CreateCustomer(...);
var customer = new CustomerBuilder().WithName("JP").Build();
```

## Code coverage

- line coverage
- method coverage
  - of een methode is aangeroepen
- block/branch coverage
  `if` `else` `ternary` `for` `while` alles met `{}` eigenlijk `try`-`catch`
- statement coverage
  - omdat 1 regel soms meerdere statements heeft: `var x = 14; y = 28;`

## Mocken

- data faken voor de tests
- onafhankelijker testen
- vaak i.c.m. dependencies
- goed om tests lekker snel te houden

Test double (vergelijkbaar met "stunt double")
- mocks - nepgedrag
- fakes - voorgedefinieerd gedrag
- dummies - opvulparameters
- stubs - registreert informatie
- spies - alles bij elkaar?

Meestal praat men over mocks. Zo zijn er geen test double frameworks, maar zat mock frameworks.

&nbsp;

3 hoofdingrediënten van het werken met mocks

- `new Mock<I...>();`
- `.Setup().Returns()`  `.Throws<>();`
- `.Verify()`

### Zijn er zaken die je niet kan mocken?

- lastig: statics. `DateTime.Now`, `File.AppendAllTextAsync()`, etc.
- lastig: unmanaged  COM-objecten
- lastig: als de architectuur niet is ingericht op mocken (geen DI, geen interfaces)
- niet mogelijk: primitives en daarmee ook custom `struct`s. Dit laatste is bij Blazor soms een dingetje:
  ```html
  <input @ref="JouwDiv">
  ```
  ```cs
  public ElementReference MijnInput { get; set; }
  //     ^-- struct
  await MijnInput.FocusAsync();
  ```

## Mockframeworks

- Moq
- FakeItEasy
- NSubstitute
- (dood) RhinoMocks

```cs
// Moq
var mock = new Mock<IMijnService>();
mock.Setup(x => x.GetProductById(It.IsAny<int>())).Returns(new Product());
mock.Verify(x => x.GetProductById(It.IsAny<int>()), Times.Once());

// FIE
var mock = A.Fake<IMijnService>();
A.CallTo(x => x.GetProductById(A<int>._)).Returns(new Product());
A.CallTo(x => x.GetProductById(A<int>._)).MustHaveHappenedOnceExactly();

// NSubstitute
var mock = Substitute.For<IMijnService>();
mock.GetProductById(Arg.Any<int>()).Returns(new Product());
mock.Received(1).GetProductById(Arg.Any<int>());
```

## Mutation testing

Testing your tests. Het past je productiecode aan.

```cs
if (x > 4) { // productiecode
	...
}
```
```cs
if (x < 4) { // mutant
	...
}
if (x == 4) { // mutant
	...
}
if (x >= 4) { // mutant
	...
}
if (x > 4000) { // mutant
	...
}
```

Zie [Getting started](https://stryker-mutator.io/docs/stryker-net/getting-started/).

## Worst practices

```cs
try
{
   controller.DoSomething();
}
catch (Exception e) { }
```

## Overig

### Waarom JP Rider fijner vindt

- HTML/CSS/JS/TS-ondersteuning
- projecten runnen worden bij VS in aparte console schermpjes geopend
- runconfiguraties maar nog steeds 1 enkele project kunnen herstarten na runnen
- VS crasht nog wel eens
- modal dialog "Waiting for a background operation to complete"
- betere default shortcuts ctrl+/ ctrl+K ctrl+C ctrl+K ctrl+U
- $$$
- VS duurde echt lang met met 64 bit worden (en was beperkt tot 4GB RAM)
