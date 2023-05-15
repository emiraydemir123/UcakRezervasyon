using UcakRezervasyon;

var u1 = new Ucak();
u1.Id = 1;
u1.Kapasite = 50;
u1.Marka = "BOEING";

var u2 = new Ucak();
u2.Id = 2;
u2.Kapasite = 55;
u2.Marka = "AIRBUS";

var l1 = new Lokasyon();
l1.Id = 1;
l1.Ad = "ISTANBUL";
l1.Ulke = "TURKIYE";

var l2 = new Lokasyon();
l2.Id = 2;
l2.Ad = "IZMIR";
l2.Ulke = "TURKIYE";

var l3 = new Lokasyon();
l3.Id = 3;
l3.Ad = "ANKARA";
l3.Ulke = "TURKIYE";

var s1 = new Sefer();
s1.Id = 1;
s1.Kalkis = l1;
s1.Varis = l2;
s1.Zaman = new DateTime(2023, 12, 1);
s1.Tasit = u1;

var s2 = new Sefer();
s2.Id = 2;
s2.Kalkis = l1;
s2.Varis = l3;
s2.Zaman = new DateTime(2023, 12, 1);
s2.Tasit = u2;

var sefer_listesi = new List<Sefer> { s1, s2 };
var i = new Islemler(sefer_listesi);

Console.WriteLine("KALKIS YERI (BUYUK HARFLERLE): ");
var kalkis_yeri = Console.ReadLine();

Console.WriteLine("VARIS YERI (BUYUK HARFLERLE): ");
var varis_yeri = Console.ReadLine();

Console.WriteLine("TARIH (GUN BOSLUK AY BOSLUK YIL OLARAK): ");
string tarih_str = Console.ReadLine();
var tarih_array = tarih_str.Split(" ");
int gun = System.Convert.ToInt32(tarih_array[0]);
int ay = System.Convert.ToInt32(tarih_array[1]);
int yil = System.Convert.ToInt32(tarih_array[2]);
var tarih = new DateTime(yil, ay, gun);

var seferler = i.GetirSeferListesiKalkisVeVarisVeTariheGore(kalkis_yeri, varis_yeri, tarih);
if (seferler.Count() == 0)
{
    Console.WriteLine("BOYLE BIR SEFER YOK");
}
else
{
    Console.WriteLine("MEVCUT SEFERLER LISTELENMEKTEDIR");
    Console.WriteLine("SEFER NO/KALKIS/VARIS/TARIH/KAPASITE/BOS KAPASITE");

    foreach (var s in seferler)
    {
        var bos_koltuk = i.GetirBosKoltukSayisiSefereGore(s);

        Console.WriteLine(String.Format("{0}/{1}/{2}/{3}/{4}/{5}",
                                        s.Id,
                                        s.Kalkis.Ad,
                                        s.Varis.Ad,
                                        s.Zaman.ToShortDateString(),
                                        s.Tasit.Kapasite,
                                        bos_koltuk));
    }

    Console.WriteLine("KAYIT (SEFER NO BOSLUK AD BOSLUK SOYAD BOSLUK KOLTUK NO OLARAK)");
    string kayit_str = Console.ReadLine();
    var kayit_array = kayit_str.Split(" ");
    int sefer_no = System.Convert.ToInt32(kayit_array[0]);
    string ad = kayit_array[1];
    string soyad = kayit_array[2];
    var koltuk_no = System.Convert.ToInt32(kayit_array[3]);

    var sefer = i.GetirSeferIdYeGore(sefer_no);
    i.RezervasyonYap(sefer, ad, soyad, koltuk_no);
    Console.WriteLine("REZERVASYON TAMAMLANDI");

    Console.WriteLine("SEFER NO/KALKIS/VARIS/TARIH/KAPASITE/BOS KAPASITE");

    var bos_koltuk_kayittan_sonra = i.GetirBosKoltukSayisiSefereGore(sefer);

    Console.WriteLine(String.Format("{0}/{1}/{2}/{3}/{4}/{5}",
                                    sefer.Id,
                                    sefer.Kalkis.Ad,
                                    sefer.Varis.Ad,
                                    sefer.Zaman.ToShortDateString(),
                                    sefer.Tasit.Kapasite,
                                    bos_koltuk_kayittan_sonra));
}
