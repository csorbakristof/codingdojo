# Linq to XML lekérdezés labor (codename: SVG Lab)

Ennek a labornak a célja a Linq to XML lekérdezések gyakorlása. Ehhez első körben kell egy adathalmaz, amire
utána a lekérdezésekkel megválaszolandó feladatok vonatkoznak. A szemléletesség kedvéért kihasználjuk, hogy az
SVG vektorgrafikus képformátum egy XML fájl, így a lekérdezések színes téglalapok és feliratok tulajdonságaira
fognak vonatkozni.

## A fájlok

- README.md: ez a leírás
- Solutions.cs: ebbe kell elkészíteni a megoldásokat. A feladatok valójában itteni metódusok, így ezek
fejlécéből és az előtte álló kommentárokból derül ki, mik is a tényleges feladatok.
- TaskTests.cs: a megoldásokat ellenőrző unit tesztek. Nevük megegyezik a tesztelt metódus nevével.
- ExtensionMethods.cs: bizonyos funkciók sokkal kényelmesebben használhatók, ha extension methodként
írjuk meg őket, amihez egy statikus osztály kell. Amit így szeretnél elkészíteni, annak itt van az előre
előkészített helye.
- rectangles1.svg, rectangles2.svg: XML alapú, vektorgrafikus kép, amire a unit tesztek az elkészült megoldás helyességét
tesztelik.
- rectangles1.png, rectangles2.png: PNG képként a megfelelő SVG fájlok tartalma, hátha van, akinek így könnyebb megnézni.


## A labor elvégzésének lépései

- Nézd meg a projektben lévő SVG fájlok és a mellékelt PNG megfelelőik tartalmát. Milyen attribútumok
tartalmazzák például egy téglalap szélességét és a kontúr vonalvastagságát? Az id, x, y, width, height és style
attribútumokra szükség lesz a feladatok megoldása során.
- Nézd meg a példa Linq to XML lekérdezéseket a
https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/basic-queries-linq-to-xml
oldalon.
- A labor feladatainak megoldását a Solutions.cs osztály metódusaiban készítsd el. A feladatok rövid leírása
is ezen metódusok előtt szerepel a forráskódban. A metódusok helyes működését a
TasksToComplete unit tesztjei ellenőrzik. Természetesen a kód duplikáció elkerülése érdekében a Solutions
osztályban tetszőleges további segéd metódusokat is létrehozhatsz. Ugyanazt a szűrést ne írd le kérszer!
(Ajánlásként kommentárba felsoroltam pár metódust, amit jó eséllyel megéri elkészíteni. Ha van kedved,
szorgalmi feladatként ezek elkészítése előtt kifejezetten ezeket ellenőrző, további unit teszteket is
készíthetsz.)
- Mivel most Linq alapú lekérdezéseket kell majd írni, a ciklusok (while, for, foreach) mind code smellnek
minősülnek: valószínűleg arra utalak, hogy a megoldásod nem teljesen "Linq-es", ezért amikor csak lehet, került
el őket.
- A Linq-es kifejezések egy csomó lépést egymás után hajtanak végre, amit debuggolni elég nehéz. Debug
célokra ideiglenesen nyugodtan vezess be változókat, amikbe a részeredményeket elmented. A debugger az
IEnumerable értékeit is össze tudja neked gyűjteni, de ideiglenesen nyugodtan a kifejezés végére tehetsz
egy .ToArray() hívást is.
- A fejlesztés során amint valami helyesen működik, a megfelelő unit teszt zölddé válik. Érdemes bekapcsolni
a Live Unit Testinget, mivel akkor nem kell mindig kézzel futtatgatni a unit teszteket.
- A munka során törekedj a kód duplikáció elkerülésére, ami azt is jelentheti, hogy egy korábban már elkészített
feladatot kis mértékben módosítani kell, hogy például egy másikkal közös metódust használjon. A cél a végleges
kód duplikáció mentessége, vagyis néha vissza kell emiatt térni korábbi feladatokhoz és refactorálni kell
őket. Ez ipari környezetben is így van.
