#include <stdio.h>
#include <string.h> //array işlemleri için.
#include <stdlib.h> //dosya işlemleri için.
#include <conio.h> //getch() fonk. için.
#include <time.h> //Çalışma süresi hesabı için.


typedef struct { //Öğrencileri tutmak için.
	unsigned int num;
	char ad[20];
	char soyad[20];
	int kayitsirasi;
	char ogr[3];
} Sinif;
static Sinif ogrenciler[300];

typedef struct { //Bubble sort da kullanmak için.
	unsigned int num;
	char ad[20];
	char soyad[20];
	int kayitsirasi;
	char ogr[3];
} Temp;
static Temp temp[2];

typedef struct { //Tekrar eden silinen kayitlari tutmak için.
	char ad[20];
	char soyad[20];
} Silinen;
static Silinen kayitlar[100];


typedef struct {//sınıfları tutmak için.
	int kapasite;
	int sinif_numarasi;
	int kullanılan;
} Kapasiteler;
static Kapasiteler siniflar[10];


typedef struct {//Sınıfları büyükten küçüğe sıralamak için.
	int kapasite;
	int sinif_numarasi;
} Temp2;
static Temp2 temp2[1];

static int ogr1_sayisi = 0, ogr2_sayisi = 0;
static int kayit_sira_global = 0;
static int ilk_dosya_kayit_sirasi_global = 0;

int kayit_oku(int secim) {
	FILE *filehandle = NULL;
	char lyne[1000];
	char *item;
	int reccount = 0;
	ilk_dosya_kayit_sirasi_global = 0;

	if (secim == 0)
	{
		filehandle = fopen("ogrenci_kayit_bilgileri.txt", "r");
	}
	else if (secim == 1) {
		filehandle = fopen("ogrenci_kayit_bilgileri_tekrarsil.txt", "r");
	}
	else {
		printf("Yanlis dosya secimi. Dosya yollarını kontrol et.\n");
	}

	while (fgets(lyne, 200, filehandle)) { //Referans: http://www.wellho.net/resources/ex.php4?item=c209/lunches.c

		item = strtok(lyne, " ");
		ogrenciler[reccount].num = atoi(item); //atoi:convert string to int

		item = strtok(NULL, " ");
		strcpy(ogrenciler[reccount].ad, item);

		item = strtok(NULL, " ");
		strcpy(ogrenciler[reccount].soyad, item);

		item = strtok(NULL, " ");
		ogrenciler[reccount].kayitsirasi = atoi(item);

		item = strtok(NULL, "\n");
		strcpy(ogrenciler[reccount].ogr, item);

		//printf("Numara: %u\n\n", ogrenciler[reccount].num);  //Numara Ekrana yazdırmak için
		reccount++;
	}

	fclose(filehandle);
	int max = ogrenciler[0].kayitsirasi;
	for (int i = 0; i < reccount; i++) //İlk dosyadaki en büyük kayit sirasini tespit etmek için.
	{
		if (ogrenciler[i].kayitsirasi > max) max = ogrenciler[i].kayitsirasi;
	}
	ilk_dosya_kayit_sirasi_global = max;
	return reccount;
}

int tekrar_kayit_sil(int kayit_sayisi) {
	char ad[20], soyad[20];
	int kayit_sirasi = 0, silinen_kayit_sirasi = 0;
	char temp_ad[20], temp_soyad[20];
	int silinen_sayac = 0;
	for (int i = 0; i < kayit_sayisi; i++)
	{
		strcpy(ad, ogrenciler[i].ad);
		strcpy(soyad, ogrenciler[i].soyad);
		kayit_sirasi = ogrenciler[i].kayitsirasi;
		for (int k = i + 1; k < kayit_sayisi; k++)
		{
			strcpy(temp_ad, ogrenciler[k].ad);
			strcpy(temp_soyad, ogrenciler[k].soyad);
			if (strcmp(ad, temp_ad) == 0 && strcmp(soyad, temp_soyad) == 0)//Ad soyad karşılaştırma
			{
				if (k != kayit_sayisi - 1)
				{
					kayit_sayisi = kayit_sayisi - 1;
					if (ogrenciler[k].kayitsirasi > ogrenciler[i].kayitsirasi) //Aşağıdaki tekrarda kayıt sırası büyükse
					{
						silinen_kayit_sirasi = ogrenciler[k].kayitsirasi;
						for (int j = k; j < kayit_sayisi; j++) // Tekrar eden kayıttan sonraki kayıtları bi yukarı taşıma
						{
							ogrenciler[j].num = ogrenciler[j + 1].num;
							strcpy(ogrenciler[j].ad, ogrenciler[j + 1].ad);
							strcpy(ogrenciler[j].soyad, ogrenciler[j + 1].soyad);
							ogrenciler[j].kayitsirasi = ogrenciler[j + 1].kayitsirasi;
							strcpy(ogrenciler[j].ogr, ogrenciler[j + 1].ogr);
						}
						for (int m = 0; m < kayit_sayisi; m++) //Silinen kayıtın sıra numarasından büyük kayıt sıralarını 1 azaltma
						{
							if (ogrenciler[m].kayitsirasi != 0 && ogrenciler[m].kayitsirasi > silinen_kayit_sirasi)
								ogrenciler[m].kayitsirasi--;
						}

						strcpy(kayitlar[silinen_sayac].ad, ad); //Silinen isimleri hafızada tutma
						strcpy(kayitlar[silinen_sayac].soyad, soyad);
						silinen_sayac++;

						ogrenciler[kayit_sayisi].num = NULL; // Kayit sayisi azaltıldığı için sondaki fazla kayıt temizlenir.
						strcpy(ogrenciler[kayit_sayisi].ad, " ");
						strcpy(ogrenciler[kayit_sayisi].soyad, " ");
						ogrenciler[kayit_sayisi].kayitsirasi = NULL;
						strcpy(ogrenciler[kayit_sayisi].ogr, " ");
					}
					else if (ogrenciler[k].kayitsirasi < ogrenciler[i].kayitsirasi) //Aşağıdaki tekrarın kayıt sırası küçükse
					{
						silinen_kayit_sirasi = ogrenciler[i].kayitsirasi;
						for (int j = i; j < kayit_sayisi; j++) // Tekrar eden kayıttan sonraki kayıtları bi yukarı taşıma
						{
							ogrenciler[j].num = ogrenciler[j + 1].num;
							strcpy(ogrenciler[j].ad, ogrenciler[j + 1].ad);
							strcpy(ogrenciler[j].soyad, ogrenciler[j + 1].soyad);
							ogrenciler[j].kayitsirasi = ogrenciler[j + 1].kayitsirasi;
							strcpy(ogrenciler[j].ogr, ogrenciler[j + 1].ogr);
						}
						for (int m = 0; m < kayit_sayisi; m++) //Silinen kayıtın sıra numarasından büyük kayıt sıralarını 1 azaltma
						{
							if (ogrenciler[m].kayitsirasi != 0 && ogrenciler[m].kayitsirasi > silinen_kayit_sirasi)
								ogrenciler[m].kayitsirasi--;
						}

						strcpy(kayitlar[silinen_sayac].ad, ad); //Silinen isimleri hafızada tutma
						strcpy(kayitlar[silinen_sayac].soyad, soyad);
						silinen_sayac++;

						ogrenciler[kayit_sayisi].num = NULL; // Kayit sayisi azaltıldığı için sondaki fazla kayıt temizlenir.
						strcpy(ogrenciler[kayit_sayisi].ad, " ");
						strcpy(ogrenciler[kayit_sayisi].soyad, " ");
						ogrenciler[kayit_sayisi].kayitsirasi = NULL;
						strcpy(ogrenciler[kayit_sayisi].ogr, " ");
						i--;
					}
				}
				else { //Kayıtlı öğrencilerin sonuna gelindiyse
					kayit_sayisi = kayit_sayisi - 1;
					ogrenciler[kayit_sayisi].num = NULL;
					strcpy(ogrenciler[kayit_sayisi].ad, " ");
					strcpy(ogrenciler[kayit_sayisi].soyad, " ");
					ogrenciler[kayit_sayisi].kayitsirasi = NULL;
					strcpy(ogrenciler[kayit_sayisi].ogr, " ");

					strcpy(kayitlar[silinen_sayac].ad, ad);
					strcpy(kayitlar[silinen_sayac].soyad, soyad);
					silinen_sayac++;
				}

			}
		}
	}


	printf("2 defa ismi gecen ogrencilerin tekrarlari silinmistir.\n");
	printf("Isimler:\n");
	for (int i = 0; i < silinen_sayac; i++)
	{
		printf("%s %s\n", kayitlar[i].ad, kayitlar[i].soyad);
	}

	printf("\n2'den fazla tekrar eden ogrenci yoktur.\n\n");
	printf("Kayit siralari guncellenmistir...\n");
	FILE *f = fopen("ogrenci_kayit_bilgileri_tekrarsil.txt", "w");
	if (f == NULL)
	{
		printf("Error opening file!\n");
		exit(1);
	}
	for (int k = 0; k < kayit_sayisi; k++)
	{
		/* print some text */
		fprintf(f, "%u %s %s %d %s\n", ogrenciler[k].num, ogrenciler[k].ad, ogrenciler[k].soyad, ogrenciler[k].kayitsirasi, ogrenciler[k].ogr);
	}
	fclose(f);
	return kayit_sayisi;
}

static int count_ogr1 = 0, count_ogr2 = 0;
void ogr_numarasi_ata(int kayit_sayisi) {
	//char ogr_num1[8], ogr_num2[8];
	char ogr[3], ogr1[2], ogr2[3];
	strcpy(ogr1, "I");
	strcpy(ogr2, "II");
	//strcpy(ogr_num1, "1701001");
	//strcpy(ogr_num2, "1702001");
	unsigned int ogr_num = 0, sayi1 = 0, sayi2 = 0;
	int kayit_sirasi = 0, toplam_kayit_sirasi = 0;
	//sayi1 = (unsigned int)(strtoull(ogr_num1, NULL, 0)); //Referance: https://stackoverflow.com/questions/13025441/how-to-convert-the-char-array-to-unsigned-int
	//sayi2 = (unsigned int)(strtoull(ogr_num2, NULL, 0));
	sayi1 = 1701001;
	sayi2 = 1702001;

	int min = 0, max = 0;

	min = 999;
	max = ogrenciler[0].kayitsirasi;
	for (int i = 0; i < kayit_sayisi; i++)
	{
		if (ogrenciler[i].kayitsirasi != 0) //min ve max kayit sirasını bulmak için
		{
			if (ogrenciler[i].kayitsirasi < min) min = ogrenciler[i].kayitsirasi;
			if (ogrenciler[i].kayitsirasi > max) max = ogrenciler[i].kayitsirasi;
		}
	}
	kayit_sirasi = min;
	while (kayit_sirasi <= max)
	{
		for (int i = 0; i < kayit_sayisi; i++)
		{
			if (ogrenciler[i].kayitsirasi == kayit_sirasi)
			{
				ogr_num = ogrenciler[i].num;
				strcpy(ogr, ogrenciler[i].ogr);
				if (ogr_num == 0 && strcmp(ogr, ogr1) == 0) // 1. öğretim numara atama
				{
					if (count_ogr1 >= 0 && count_ogr1 < 1000)
					{
						ogrenciler[i].num = sayi1 + count_ogr1;
					}
					else {
						printf("1. öğretim numara sayisi 999'dan fazla.\n");
					}
					count_ogr1++;
				}
				else if (ogr_num == 0 && strcmp(ogr, ogr2) == 0) //2. Öğretim Numara Atama
				{
					if (count_ogr2 >= 0 && count_ogr2 < 1000)
					{
						ogrenciler[i].num = sayi2 + count_ogr2;
					}
					else {
						printf("2. öğretim numara sayisi 999'dan fazla.\n");
					}
					count_ogr2++;
				}
			}
		}
		kayit_sirasi++;
	}
}

// Kendim kullandım. 1. ve 2. öğretim öğrenci sayilari tespit edildi.
void kayitlari_ayir(int kayit_sayisi) { 
	char ogr[3], ogr1[2], ogr2[3];
	strcpy(ogr1, "I");
	strcpy(ogr2, "II");
	ogr1_sayisi = 0;
	ogr2_sayisi = 0;

	FILE *f1 = fopen("ogr1.txt", "w");
	if (f1 == NULL)
	{
		printf("Error opening file!\n");
		exit(1);
	}
	FILE *f2 = fopen("ogr2.txt", "w");
	if (f2 == NULL)
	{
		printf("Error opening file!\n");
		exit(1);
	}


	for (int k = 0; k < kayit_sayisi; k++)
	{
		strcpy(ogr, ogrenciler[k].ogr);
		if (strcmp(ogr, ogr1) == 0)
		{
			fprintf(f1, "%u %s %s %d %s\n", ogrenciler[k].num, ogrenciler[k].ad, ogrenciler[k].soyad, ogrenciler[k].kayitsirasi, ogrenciler[k].ogr);
			ogr1_sayisi++;
		}
		else if (strcmp(ogr, ogr2) == 0)
		{
			fprintf(f2, "%u %s %s %d %s\n", ogrenciler[k].num, ogrenciler[k].ad, ogrenciler[k].soyad, ogrenciler[k].kayitsirasi, ogrenciler[k].ogr);
			ogr2_sayisi++;
		}
		else {
			printf("Dosyada hata!\n");
		}
	}
	fclose(f1);
	fclose(f2);
	//printf("Ogr1 ve Ogr2 olarak kayit tamamlandi.\n");
}

void numara_sirasi(int kayit_sayisi) { //http://www.sanfoundry.com/c-program-sorting-bubble-sort/
	unsigned int min = 1800000;
	for (int i = 0; i < kayit_sayisi - 1; i++)
	{
		for (int j = 0; j < (kayit_sayisi - i - 1); j++)
		{
			if (ogrenciler[j].num > ogrenciler[j + 1].num) { //Bubble Sort
				temp[0].num = ogrenciler[j].num;
				strcpy(temp[0].ad, ogrenciler[j].ad);
				strcpy(temp[0].soyad, ogrenciler[j].soyad);
				temp[0].kayitsirasi = ogrenciler[j].kayitsirasi;
				strcpy(temp[0].ogr, ogrenciler[j].ogr);

				ogrenciler[j].num = ogrenciler[j + 1].num;
				strcpy(ogrenciler[j].ad, ogrenciler[j + 1].ad);
				strcpy(ogrenciler[j].soyad, ogrenciler[j + 1].soyad);
				ogrenciler[j].kayitsirasi = ogrenciler[j + 1].kayitsirasi;
				strcpy(ogrenciler[j].ogr, ogrenciler[j + 1].ogr);

				ogrenciler[j + 1].num = temp[0].num;
				strcpy(ogrenciler[j + 1].ad, temp[0].ad);
				strcpy(ogrenciler[j + 1].soyad, temp[0].soyad);
				ogrenciler[j + 1].kayitsirasi = temp[0].kayitsirasi;
				strcpy(ogrenciler[j + 1].ogr, temp[0].ogr);
			}
		}
	}

	FILE *f = fopen("ogrenci_kayit_bilgileri_numara_sirasi.txt", "w");
	if (f == NULL)
	{
		printf("Error opening file!\n");
		exit(1);
	}
	for (int k = 0; k < kayit_sayisi; k++)
	{
		/* print some text */
		fprintf(f, "%u %s %s %d %s\n", ogrenciler[k].num, ogrenciler[k].ad, ogrenciler[k].soyad, ogrenciler[k].kayitsirasi, ogrenciler[k].ogr);
	}
	fclose(f);
}

clock_t saat5_basla;
clock_t saat5_bitir;
void sinif_dagit(int kayit_sayisi) {
	FILE *file;
	int sinif_sayisi = 0;
	int toplam_sinif = 0;
	int dosyaya_yazilan_ogr_sayisi = 0;
	int toplam_ogr1 = ogr1_sayisi;
	int toplam_ogr2 = ogr2_sayisi;
	printf("\nDersi alan ogrenci sayisi 1. ogretim: %d, 2. ogretim: %d'dir.\n", ogr1_sayisi, ogr2_sayisi);
	
	//Sinif kapasiteleri.
	do
	{ //Sinif kapasite toplamı öğrenci sayisina eşit olana kadar devam et.
		toplam_sinif = 0;
		printf("\nSinif Sayisini Giriniz: ");
		scanf("%d", &sinif_sayisi);

		printf("Sinif Kapasitelerini Giriniz: ");
		for (int i = 0; i < sinif_sayisi; i++)
		{
			siniflar[i].sinif_numarasi = i;
			siniflar[i].kullanılan = 0;
			/*printf("%d. sinif: ", i + 1);*/
			scanf("%d", &siniflar[i].kapasite);
			toplam_sinif = toplam_sinif + siniflar[i].kapasite;
		}
		if (toplam_sinif < ogr1_sayisi || toplam_sinif < ogr2_sayisi)
			printf("\nSinif kapasiteleri yeterli degildir.\n");
	} while (!(toplam_sinif >= ogr1_sayisi && toplam_sinif >= ogr2_sayisi));

	saat5_basla = clock();

	int id = 0, count = 0;
	//printf("..... 2. ogretim: %d %d %d %d.", ort_ogr2, ort_ogr2, ort_ogr2, ogr2_sayisi - ort_ogr2);
	int en_kucuk = siniflar[0].kapasite;
	int en_kucuk_index = 0;

	//Siniflari kapasiteye göre sıralama.
	for (int c = 0; c < (sinif_sayisi - 1); c++)
	{
		for (int d = 0; d < sinif_sayisi - c - 1; d++)
		{
			if (siniflar[d].kapasite < siniflar[d + 1].kapasite) 
			{
				temp2[0].kapasite = siniflar[d].kapasite;
				temp2[0].sinif_numarasi = siniflar[d].sinif_numarasi;

				siniflar[d].kapasite = siniflar[d + 1].kapasite;
				siniflar[d].sinif_numarasi = siniflar[d + 1].sinif_numarasi;

				siniflar[d + 1].kapasite = temp2[0].kapasite;
				siniflar[d + 1].sinif_numarasi = temp2[0].sinif_numarasi;
			}
		}
	}
	int a = 1;
	int toplam_kapasite = 0;
	// En az sınıf 1. öğretim
	for (int i = 0; i < sinif_sayisi; i++) 
	{
		if (toplam_ogr1 > 0)
		{
			if (toplam_ogr1 > siniflar[i].kapasite) {

				char buffer[32]; // The filename buffer.
								 // Put "file" then k then ".txt" in to filename.
				snprintf(buffer, sizeof(char) * 32, "sinif%iogr1-enaz.txt", siniflar[i].sinif_numarasi + 1);
				//Reference: https://stackoverflow.com/questions/4232842/how-to-dynamically-change-filename-while-writing-in-a-loop

				// here we get some data into variable data
				toplam_kapasite = toplam_kapasite + siniflar[i].kapasite;
				file = fopen(buffer, "wb");
				for (int k = count; k < toplam_kapasite; k++)
				{
					if (strcmp(ogrenciler[k].ogr, "I") == 0)
					{
						fprintf(file, "%u %s %s %d %s\n", ogrenciler[k].num, ogrenciler[k].ad, ogrenciler[k].soyad, ogrenciler[k].kayitsirasi, ogrenciler[k].ogr);
						siniflar[i].kullanılan++;
					}
					else
					{
						toplam_kapasite++;
					}
					count++;
					//fwrite(data, 1, strlen(data), file);
				}
				fclose(file);

			}
			else
			{
				char buffer[32]; // The filename buffer.
								 // Put "file" then k then ".txt" in to filename.
				snprintf(buffer, sizeof(char) * 32, "sinif%iogr1-enaz.txt", siniflar[i].sinif_numarasi + 1);
				//Reference: https://stackoverflow.com/questions/4232842/how-to-dynamically-change-filename-while-writing-in-a-loop

				// here we get some data into variable data
				toplam_kapasite = toplam_kapasite + toplam_ogr1;
				file = fopen(buffer, "wb");
				for (int k = count; k < toplam_kapasite; k++)
				{
					if (strcmp(ogrenciler[k].ogr, "I") == 0)
					{
						fprintf(file, "%u %s %s %d %s\n", ogrenciler[k].num, ogrenciler[k].ad, ogrenciler[k].soyad, ogrenciler[k].kayitsirasi, ogrenciler[k].ogr);
						siniflar[i].kullanılan++;
					}
					else
					{
						toplam_kapasite++;
					}
					count++;
				}

				fclose(file);
			}

			a++;
			toplam_ogr1 = toplam_ogr1 - siniflar[i].kapasite;
		}
		else
		{
			siniflar[i].kullanılan = 0;
		}
	}

	printf("\nEn az sinif dagilimi 1. ogretim - ");
	for (int m = 0; m < sinif_sayisi; m++)
	{
		for (int n = 0; n < sinif_sayisi; n++)
		{
			if (siniflar[n].sinif_numarasi == m) {
				printf("%d ", siniflar[n].kullanılan);
				siniflar[n].kullanılan = 0;
			}
		}
	}

	//int a = 1;
	toplam_kapasite = 0;
	count = 0;

	// En az sınıf dağılımı 2. öğretim
	for (int i = 0; i < sinif_sayisi; i++) 
	{
		if (toplam_ogr2 > 0)
		{
			if (toplam_ogr2 > siniflar[i].kapasite) {

				char buffer[32]; // The filename buffer.
								 // Put "file" then k then ".txt" in to filename.
				snprintf(buffer, sizeof(char) * 32, "sinif%iogr2-enaz.txt", siniflar[i].sinif_numarasi + 1);
				//Reference: https://stackoverflow.com/questions/4232842/how-to-dynamically-change-filename-while-writing-in-a-loop

				// here we get some data into variable data
				toplam_kapasite = toplam_kapasite + siniflar[i].kapasite;
				file = fopen(buffer, "wb");
				for (int k = count; k < toplam_kapasite; k++)
				{
					if (strcmp(ogrenciler[k].ogr, "II") == 0)
					{
						fprintf(file, "%u %s %s %d %s\n", ogrenciler[k].num, ogrenciler[k].ad, ogrenciler[k].soyad, ogrenciler[k].kayitsirasi, ogrenciler[k].ogr);
						siniflar[i].kullanılan++;
					}
					else
					{
						toplam_kapasite++;
					}
					count++;
					//fwrite(data, 1, strlen(data), file);
				}
				fclose(file);

			}
			else
			{
				char buffer[32]; // The filename buffer.
								 // Put "file" then k then ".txt" in to filename.
				snprintf(buffer, sizeof(char) * 32, "sinif%iogr2-enaz.txt", siniflar[i].sinif_numarasi + 1);
				//Reference: https://stackoverflow.com/questions/4232842/how-to-dynamically-change-filename-while-writing-in-a-loop

				// here we get some data into variable data
				toplam_kapasite = toplam_kapasite + toplam_ogr2;
				file = fopen(buffer, "wb");
				for (int k = count; k < toplam_kapasite; k++)
				{
					if (strcmp(ogrenciler[k].ogr, "II") == 0)
					{
						fprintf(file, "%u %s %s %d %s\n", ogrenciler[k].num, ogrenciler[k].ad, ogrenciler[k].soyad, ogrenciler[k].kayitsirasi, ogrenciler[k].ogr);
						siniflar[i].kullanılan++;
					}
					else
					{
						toplam_kapasite++;
					}
					count++;
				}

				fclose(file);
			}

			a++;
			toplam_ogr2 = toplam_ogr2 - siniflar[i].kapasite;
		}
		else
		{
			siniflar[i].kullanılan = 0;
		}
	}

	printf("........ 2. ogretim - ");
	for (int m = 0; m < sinif_sayisi; m++)
	{
		for (int n = 0; n < sinif_sayisi; n++)
		{
			if (siniflar[n].sinif_numarasi == m) {
				printf("%d ", siniflar[n].kullanılan);
				siniflar[n].kullanılan = 0;
			}
		}
	}



	int ort_ogr1 = 0, ort_ogr2 = 0;

	ort_ogr1 = ogr1_sayisi / sinif_sayisi;
	ort_ogr2 = ogr2_sayisi / sinif_sayisi;
	int temp_sinif_sayisi = sinif_sayisi;
	toplam_ogr1 = ogr1_sayisi;
	toplam_ogr2 = ogr2_sayisi;

	toplam_kapasite = 0;
	count = 0;
	bool ort_tekrar = false;
	bool kucuk_var_mi = false;

	//Eşit dağılım 1. öğretim
	while (temp_sinif_sayisi != 0) 
	{
		for (int i = sinif_sayisi - 1; i >= 0; i--)
		{
			if (kucuk_var_mi != true)
			{
				for (int j = 0; j < sinif_sayisi; j++)
				{
					if (ort_ogr1 > siniflar[j].kapasite && siniflar[j].kullanılan == 0) {
						char buffer[32]; // The filename buffer.
										 // Put "file" then k then ".txt" in to filename.
						snprintf(buffer, sizeof(char) * 32, "sinif%iogr1-esit.txt", siniflar[j].sinif_numarasi + 1);
						//Reference: https://stackoverflow.com/questions/4232842/how-to-dynamically-change-filename-while-writing-in-a-loop

						// here we get some data into variable data
						toplam_kapasite = toplam_kapasite + siniflar[j].kapasite;
						file = fopen(buffer, "wb");
						for (int k = count; k < toplam_kapasite; k++)
						{
							if (strcmp(ogrenciler[k].ogr, "I") == 0)
							{
								fprintf(file, "%u %s %s %d %s\n", ogrenciler[k].num, ogrenciler[k].ad, ogrenciler[k].soyad, ogrenciler[k].kayitsirasi, ogrenciler[k].ogr);
								siniflar[j].kullanılan++;
							}
							else
							{
								toplam_kapasite++;
							}
							count++;
							//fwrite(data, 1, strlen(data), file);
						}
						fclose(file);
						temp_sinif_sayisi--;
						toplam_ogr1 = toplam_ogr1 - siniflar[j].kullanılan;
					}
					if (j == 3) kucuk_var_mi = true;
				}
			}
			if (ort_tekrar == false)
			{
				ort_ogr1 = toplam_ogr1 / temp_sinif_sayisi;
				ort_tekrar = true;
			}
			if (siniflar[i].kullanılan == 0) {
				char buffer[32]; // The filename buffer.
								 // Put "file" then k then ".txt" in to filename.
				snprintf(buffer, sizeof(char) * 32, "sinif%iogr1-esit.txt", siniflar[i].sinif_numarasi + 1);
				//Reference: https://stackoverflow.com/questions/4232842/how-to-dynamically-change-filename-while-writing-in-a-loop

				// here we get some data into variable data
				if (toplam_ogr1 / ort_ogr1 > 1)
				{
					toplam_kapasite = toplam_kapasite + ort_ogr1;
				}
				else {
					toplam_kapasite = toplam_kapasite + toplam_ogr1;
				}
				file = fopen(buffer, "wb");
				for (int k = count; k < toplam_kapasite; k++)
				{
					if (strcmp(ogrenciler[k].ogr, "I") == 0)
					{
						fprintf(file, "%u %s %s %d %s\n", ogrenciler[k].num, ogrenciler[k].ad, ogrenciler[k].soyad, ogrenciler[k].kayitsirasi, ogrenciler[k].ogr);
						siniflar[i].kullanılan++;
					}
					else
					{
						toplam_kapasite++;
					}
					count++;
					//fwrite(data, 1, strlen(data), file);
				}
				fclose(file);
				temp_sinif_sayisi--;
				toplam_ogr1 = toplam_ogr1 - siniflar[i].kullanılan;
			}
		}
	}

	printf("\n\nEsit dagilim: 1. ogretim - ");
	for (int m = 0; m < sinif_sayisi; m++)
	{
		for (int n = 0; n < sinif_sayisi; n++)
		{
			if (siniflar[n].sinif_numarasi == m) {
				printf("%d ", siniflar[n].kullanılan);
				siniflar[n].kullanılan = 0;
			}
		}
	}

	toplam_kapasite = 0;
	count = 0;
	temp_sinif_sayisi = sinif_sayisi;
	ort_tekrar = false;
	kucuk_var_mi = false;


	//Eşit dağılım 2. öğretim.
	while (temp_sinif_sayisi != 0)
	{
		for (int i = sinif_sayisi - 1; i >= 0; i--)
		{
			if (kucuk_var_mi != true)
			{
				for (int j = 0; j < sinif_sayisi; j++)
				{
					if (ort_ogr2 > siniflar[j].kapasite && siniflar[j].kullanılan == 0) {
						char buffer[32]; // The filename buffer.
										 // Put "file" then k then ".txt" in to filename.
						snprintf(buffer, sizeof(char) * 32, "sinif%iogr2-esit.txt", siniflar[j].sinif_numarasi + 1);
						//Reference: https://stackoverflow.com/questions/4232842/how-to-dynamically-change-filename-while-writing-in-a-loop

						// here we get some data into variable data
						toplam_kapasite = toplam_kapasite + siniflar[j].kapasite;
						file = fopen(buffer, "wb");
						for (int k = count; k < toplam_kapasite; k++)
						{
							if (strcmp(ogrenciler[k].ogr, "II") == 0)
							{
								fprintf(file, "%u %s %s %d %s\n", ogrenciler[k].num, ogrenciler[k].ad, ogrenciler[k].soyad, ogrenciler[k].kayitsirasi, ogrenciler[k].ogr);
								siniflar[j].kullanılan++;
							}
							else
							{
								toplam_kapasite++;
							}
							count++;
							//fwrite(data, 1, strlen(data), file);
						}
						fclose(file);
						temp_sinif_sayisi--;
						toplam_ogr2 = toplam_ogr2 - siniflar[j].kullanılan;
					}
					if (j == 3) kucuk_var_mi = true;
				}
			}
			if (ort_tekrar == false)
			{
				ort_ogr2 = toplam_ogr2 / temp_sinif_sayisi;
				ort_tekrar = true;
			}
			if (siniflar[i].kullanılan == 0) {
				char buffer[32]; // The filename buffer.
								 // Put "file" then k then ".txt" in to filename.
				snprintf(buffer, sizeof(char) * 32, "sinif%iogr2-esit.txt", siniflar[i].sinif_numarasi + 1);
				//Reference: https://stackoverflow.com/questions/4232842/how-to-dynamically-change-filename-while-writing-in-a-loop

				// here we get some data into variable data
				if (toplam_ogr2 / ort_ogr2 > 1)
				{
					toplam_kapasite = toplam_kapasite + ort_ogr2;
				}
				else {
					toplam_kapasite = toplam_kapasite + toplam_ogr2;
				}
				file = fopen(buffer, "wb");
				for (int k = count; k < toplam_kapasite; k++)
				{
					if (strcmp(ogrenciler[k].ogr, "II") == 0)
					{
						fprintf(file, "%u %s %s %d %s\n", ogrenciler[k].num, ogrenciler[k].ad, ogrenciler[k].soyad, ogrenciler[k].kayitsirasi, ogrenciler[k].ogr);
						siniflar[i].kullanılan++;
					}
					else
					{
						toplam_kapasite++;
					}
					count++;
					//fwrite(data, 1, strlen(data), file);
				}
				fclose(file);
				temp_sinif_sayisi--;
				toplam_ogr2 = toplam_ogr2 - siniflar[i].kullanılan;
			}
		}
	}

	printf("........ 2. ogretim - ");
	for (int m = 0; m < sinif_sayisi; m++)
	{
		for (int n = 0; n < sinif_sayisi; n++)
		{
			if (siniflar[n].sinif_numarasi == m) {
				printf("%d ", siniflar[n].kullanılan);
				siniflar[n].kullanılan = 0;
			}
		}
	}
	printf("\n\nTum ogrenciler yerlestirilebilmistir.");
	saat5_bitir = clock();
}

//Numara sırasına göre düzenlenen listede max kayit sirasi bulmak için.
void kayit_sira_bul(int kayit_sayisi) {
	int max = ogrenciler[0].kayitsirasi;
	for (int i = 0; i < kayit_sayisi; i++)
	{
		if (ogrenciler[i].kayitsirasi > max)
			max = ogrenciler[i].kayitsirasi;
	}
	kayit_sira_global = max + 1;
}

int ogrenci_ekle(int sayi, int kayit_sayisi) {
	char ad[20], soyad[20], ogr_turu[3];
	kayit_sira_bul(kayit_sayisi);

	FILE *dosya;
	char buffer[256];

	dosya = fopen("ogrenci_kayit_bilgileri.txt", "a");
	if (dosya == NULL) {
		perror("Error opening file.");
	}

	for (int i = 0; i < sayi; i++)
	{
		printf("\nAd: "); scanf("%s", &ad);
		printf("Soyad: "); scanf("%s", &soyad);
		printf("Ogretim Turu: "); scanf("%s", &ogr_turu);

		strcpy(ogrenciler[kayit_sayisi].ad, ad);
		strcpy(ogrenciler[kayit_sayisi].soyad, soyad);
		strcpy(ogrenciler[kayit_sayisi].ogr, ogr_turu);
		ogrenciler[kayit_sayisi].kayitsirasi = kayit_sira_global;

		//Değerleri yan yana yazma.
		snprintf(buffer, sizeof buffer, "\n- %s %s %d %s", ad, soyad,ilk_dosya_kayit_sirasi_global,ogr_turu);
		fprintf(dosya, "%s", buffer);

		kayit_sayisi++;
		kayit_sira_global++;
		ilk_dosya_kayit_sirasi_global++;
	}
	fclose(dosya);
	tekrar_kayit_sil(kayit_sayisi);
	ogr_numarasi_ata(kayit_sayisi);
	numara_sirasi(kayit_sayisi);
	kayitlari_ayir(kayit_sayisi);
	sinif_dagit(kayit_sayisi);
	return kayit_sayisi;
}

int main() {
	clock_t basla = clock();

	int kayit_sayisi = 0;

	clock_t saat1_basla = clock();
	kayit_sayisi = kayit_oku(0); //verilen txt'yi oku.
	clock_t saat1_bitir = clock();

	clock_t saat2_basla = clock();
	kayit_sayisi = tekrar_kayit_sil(kayit_sayisi); //tekrar eden kayitlari sil.
	clock_t saat2_bitir = clock();

	//kayit_sayisi = kayit_oku(1); //silinen kayitlardan sonra kaydedilen txt den isimleri okuma.

	clock_t saat3_basla = clock();
	ogr_numarasi_ata(kayit_sayisi); //numarası olmayan öğrencilere numara atama.
	clock_t saat3_bitir = clock();


	clock_t saat4_basla = clock();
	numara_sirasi(kayit_sayisi);//Listeyi numara sırasına göre sıralama
	clock_t saat4_bitir = clock();


	kayitlari_ayir(kayit_sayisi);//1. öğretim ve 2. öğretim öğrencilerini ayırma.


	sinif_dagit(kayit_sayisi);//Sınıflara dağılım

	/*printf("Ogrenci Kayitlari\n");
	for (int k = 0; k < kayit_sayisi; k++) {
		printf("%u %s %s %d %s\n", ogrenciler[k].num, ogrenciler[k].ad, ogrenciler[k].soyad, ogrenciler[k].kayitsirasi, ogrenciler[k].ogr);
	}*/


	clock_t bitis = clock();

	double sure1 = (double)(saat1_bitir - saat1_basla) / CLOCKS_PER_SEC;
	double sure2 = (double)(saat2_bitir - saat2_basla) / CLOCKS_PER_SEC;
	double sure3 = (double)(saat3_bitir - saat3_basla) / CLOCKS_PER_SEC;
	double sure4 = (double)(saat4_bitir - saat4_basla) / CLOCKS_PER_SEC;
	double sure5 = (double)(saat5_bitir - saat5_basla) / CLOCKS_PER_SEC;
	double sure = (double)(bitis - basla) / CLOCKS_PER_SEC;

	printf("\n\n\n1. Madde - Kayitlari dosyadan oku: %f sn", sure1);
	printf("\n2. Madde - Birden fazla kayit sil suresi: %f sn", sure2);
	printf("\n3. Madde - Numara atama suresi: %f sn", sure3);
	printf("\n4. Madde - Numara sirasina dizme suresi: %f sn", sure4);
	printf("\n5. Madde - Siniflara yerlestirme suresi: %f sn", sure5);

	printf("\nToplam calisma suresi: %f sn", sure);

	int choose = 0, sayi = 0;
	do
	{
		printf("\n\nOgrenci eklemek istiyor musunuz? E->0, H->1: ");
		scanf(" %d", &choose);
		if (choose == 0)
		{
			printf("Kaydi yapilacak ogrenci sayisi: ");
			scanf("%d", &sayi);
			kayit_sayisi = ogrenci_ekle(sayi, kayit_sayisi);
			printf("\n%d ogrencinin kaydi basarili bir sekilde yapilmistir...", sayi);
		}
	} while (choose != 1);
	//getch();
	return 0;
}