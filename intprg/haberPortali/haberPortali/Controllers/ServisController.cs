using haberPortali.Models;
using haberPortali.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace haberPortali.Controllers
{
    public class ServisController : ApiController
    {
        DB1Entities1 db = new DB1Entities1();
        SonucModel sonuc = new SonucModel();

        #region Kategori

        [HttpGet]
        [Route("api/kategoriliste")]
        [Authorize]
        public List<KategoriModel> KategoriListe()
        {
            List<KategoriModel> liste = db.Kategori.Select(x => new KategoriModel()
            {
                KategoriId = x.KategoriId,
                KategoriAdi = x.KategoriAdi
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/kategoribyid/{kategoriId}")]
        public KategoriModel KategoriById(int kategoriId)
        {
            KategoriModel kayit = db.Kategori.Where(s => s.KategoriId == kategoriId).Select(x => new KategoriModel()
            {
                KategoriId = x.KategoriId,
                KategoriAdi = x.KategoriAdi,
            }).SingleOrDefault();

            return kayit;
        }
        [HttpPost]
        [Route("api/kategoriekle")]
        public SonucModel KategoriEkle(KategoriModel model)
        {
            if (db.Kategori.Count(s => s.KategoriAdi == model.KategoriAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kategori Adı Kayıtlıdır!";

                return sonuc;
            }
            Kategori yeni = new Kategori();
            yeni.KategoriAdi = model.KategoriAdi;
            db.Kategori.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/kategoriduzenle")]
        public SonucModel KategoriDuzenle(KategoriModel model)
        {
            Kategori kayit = db.Kategori.Where(s => s.KategoriId == model.KategoriId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";

                return sonuc;
            }
            kayit.KategoriAdi = model.KategoriAdi; db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori Düzenlendi";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/kategorisil/{kategoriId}")]
        public SonucModel KategoriSil(int KategoriId)
        {
            Kategori kayit = db.Kategori.Where(s => s.KategoriId == KategoriId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";

                return sonuc;
            }
            if (db.Haber.Count(s => s.haberKategoriId == KategoriId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Haber Kayıtlı Kategori Silinemez!";

                return sonuc;
            }
            db.Kategori.Remove(kayit); db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi";
            return sonuc;
        }
        #endregion

        #region Uye

        [HttpGet]
        [Route("api/uyeliste")]
        public List<UyeModel> UyeListe()
        {
            List<UyeModel> liste = db.Uye.Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
                adSoyad = x.adSoyad,
                email = x.email,
                kullaniciAdi = x.kullaniciAdi,
                fotograf = x.fotograf,
                sifre = x.sifre,
                uyeAdmin = x.uyeAdmin
            }).ToList();

            return liste;
        }
        [HttpGet]
        [Route("api/uyebyid/{uyeId}")]
        public UyeModel UyeById(int uyeId)
        {
            UyeModel kayit = db.Uye.Where(s => s.uyeId == uyeId).Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
                adSoyad = x.adSoyad,
                email = x.email,
                kullaniciAdi = x.kullaniciAdi,
                fotograf = x.fotograf,
                sifre = x.sifre,
                uyeAdmin = x.uyeAdmin
            }).SingleOrDefault();

            return kayit;
        }
        [HttpPost]
        [Route("api/uyeekle")]
        public SonucModel UyeEkle(UyeModel model)
        {
            if (db.Uye.Count(s => s.kullaniciAdi == model.kullaniciAdi || s.email == model.email) > 0)
            {
                sonuc.islem = false; sonuc.mesaj = "Girilen Kullanıcı Adı veya E-Posta Adresi Kayıtlıdır!";
                return sonuc;
            }

            Uye yeni = new Uye();
            yeni.adSoyad = model.adSoyad;
            yeni.email = model.email;
            yeni.kullaniciAdi = model.kullaniciAdi;
            yeni.fotograf = model.fotograf;
            yeni.sifre = model.sifre;
            yeni.uyeAdmin = model.uyeAdmin;

            db.Uye.Add(yeni);
            db.SaveChanges(); sonuc.islem = true;
            sonuc.mesaj = "Üye Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/uyeduzenle")]
        public SonucModel UyeDuzenle(UyeModel model)
        {
            Uye kayit = db.Uye.Where(s => s.uyeId == model.uyeId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";

                return sonuc;
            }
            kayit.adSoyad = model.adSoyad;
            kayit.email = model.email;
            kayit.kullaniciAdi = model.kullaniciAdi;
            kayit.fotograf = model.fotograf;
            kayit.sifre = model.sifre;
            kayit.uyeAdmin = model.uyeAdmin;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Düzenlendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/uyesil/{uyeId}")]
        public SonucModel UyeSil(int uyeId)
        {
            Uye kayit = db.Uye.Where(s => s.uyeId == uyeId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }

            if (db.Haber.Count(s => s.haberUyeId == uyeId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Haber Kaydı Olan Üye Silinemez!";
                return sonuc;
            }
            if (db.Yorum.Count(s => s.yorumUyeId == uyeId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Yorum Kaydı Olan Üye Silinemez!";
                return sonuc;
            }
            db.Uye.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Silindi";
            return sonuc;
        }
        #endregion

        #region Yorum

        [HttpGet]
        [Route("api/yorumliste")]
        public List<YorumModel> YorumListe()
        {
            List<YorumModel> liste = db.Yorum.Select(x => new YorumModel()
            {
                yorumId = x.yorumId,
                yorumIcerik = x.yorumIcerik,
                yorumUyeId = x.yorumUyeId,
                yorumTarih = x.yorumTarih,
                yorumHaberId = x.yorumHaberId
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/yorumlistebyuyeid/{uyeId}")]
        public List<YorumModel> YorumListeByUyeId(int uyeId)
        {
            List<YorumModel> liste = db.Yorum.Where(s => s.yorumId == uyeId).Select(x => new YorumModel()
            {
                yorumId = x.yorumId,
                yorumIcerik = x.yorumIcerik,
                yorumUyeId = x.yorumUyeId,
                yorumTarih = x.yorumTarih,
                yorumHaberId = x.yorumHaberId
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/yorumlistebyhaberid/{haberId}")]
        public List<YorumModel> YorumListeByhaberId(int haberId)
        {
            List<YorumModel> liste = db.Yorum.Where(s => s.yorumHaberId == haberId).Select(x => new YorumModel()
            {
                yorumId = x.yorumId,
                yorumIcerik = x.yorumIcerik,
                yorumUyeId = x.yorumUyeId,
                yorumTarih = x.yorumTarih,
                yorumHaberId = x.yorumHaberId

            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/yorumlistesoneklenenler/{s}")]
        public List<YorumModel> YorumListeSonEklenenler(int s)
        {
            List<YorumModel> liste = db.Yorum.OrderByDescending(o => o.Haber).Take(s).Select(x => new YorumModel()
            {
                yorumId = x.yorumId,
                yorumIcerik = x.yorumIcerik,
                yorumUyeId = x.yorumUyeId,
                yorumTarih = x.yorumTarih,
                yorumHaberId = x.yorumHaberId

            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/yorumbyid/{yorumId}")]
        public YorumModel YorumById(int yorumId)
        {
            YorumModel kayit = db.Yorum.Where(s => s.yorumId == yorumId).Select(x => new YorumModel()
            {
                yorumId = x.yorumId,
                yorumIcerik = x.yorumIcerik,
                yorumUyeId = x.yorumUyeId,
                yorumTarih = x.yorumTarih,
                yorumHaberId = x.yorumHaberId

                }).SingleOrDefault();

            return kayit;
        }
        [HttpPost]
        [Route("api/yorumekle")]
        public SonucModel YorumEkle(YorumModel model)
        {
            if (db.Yorum.Count(s => s.yorumHaberId == model.yorumUyeId && s.yorumHaberId == model.yorumHaberId&& s.yorumIcerik == model.yorumIcerik) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Aynı Kişi, Aynı Habere Aynı Yorumu Yapamaz!";
                return sonuc;
            }
            Yorum yeni = new Yorum();
            yeni.yorumId = model.yorumId;
            yeni.yorumIcerik = model.yorumIcerik;
            yeni.yorumUyeId = model.yorumUyeId;
            yeni.yorumHaberId = model.yorumHaberId;
            yeni.yorumTarih = model.yorumTarih;
            db.Yorum.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yorum Eklendi";

            return sonuc;
        }
        [HttpPut]
        [Route("api/yorumduzenle")]
        public SonucModel YorumDuzenle(YorumModel model)
        {
            Yorum kayit = db.Yorum.Where(s => s.yorumId == model.yorumId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            kayit.yorumId = model.yorumId;
            kayit.yorumIcerik = model.yorumIcerik;
            kayit.yorumUyeId = model.yorumUyeId;
            kayit.yorumHaberId = model.yorumHaberId;
            kayit.yorumTarih = model.yorumTarih;

            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Yorum Düzenlendi";

            return sonuc;
        }
        [HttpDelete]
        [Route("api/yorumsil/{yorumId}")]
        public SonucModel YorumSil(int yorumId)
        {
            Yorum kayit = db.Yorum.Where(s => s.yorumId == yorumId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";

                return sonuc;
            }

            db.Yorum.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yorum Silindi";

            return sonuc;
        }

        #endregion

        #region Haber
        [HttpGet]
        [Route("api/haberliste")]

        public List<HaberModel> HaberListe()
        {
            List<HaberModel> liste = db.Haber.Select(x => new HaberModel()
            {
                haberId = x.haberId,
                haberBaslik = x.haberBaslik,
                haberDetay = x.haberDetay,
                haberKategoriId = x.haberKategoriId,
                haberTarih = x.haberTarih,
                haberOkuma = x.haberOkuma,
                haberUyeId = x.haberUyeId,
            }).ToList();

            return liste;
        }
        [HttpGet]
        [Route("api/haberlistesoneklenenler/{s}")]
        public List<HaberModel> HaberListeSonEklenenler(int s)
        {
            List<HaberModel> liste = db.Haber.OrderByDescending(o => o.haberId).Take(s).Select(x => new HaberModel()
            {
                haberId = x.haberId,
                haberBaslik = x.haberBaslik,
                haberDetay = x.haberDetay,
                haberKategoriId = x.haberKategoriId,
                haberTarih = x.haberTarih,
                haberOkuma = x.haberOkuma,
                haberUyeId = x.haberUyeId,
            }).ToList();

            return liste;
        }
        [HttpGet]
        [Route("api/haberlistebykategoriid/{kategoriId}")]
        public List<HaberModel> HaberListeBykategoriId(int kategoriId)
        {
            List<HaberModel> liste = db.Haber.Where(s => s.haberKategoriId == kategoriId).Select(x => new HaberModel()
            {
                haberId = x.haberId,
                haberBaslik = x.haberBaslik,
                haberDetay = x.haberDetay,
                haberKategoriId = x.haberKategoriId,
                haberTarih = x.haberTarih,
                haberOkuma = x.haberOkuma,
                haberUyeId = x.haberUyeId,
            }).ToList();

            return liste;
        }
        [HttpGet]
        [Route("api/haberlistebyuyeid/{uyeId}")]
        public List<HaberModel> HaberListeByUyeId(int uyeId)
        {
            List<HaberModel> liste = db.Haber.Where(s => s.haberUyeId == uyeId).Select(x => new HaberModel()
            {
                haberId = x.haberId,
                haberBaslik = x.haberBaslik,
                haberDetay = x.haberDetay,
                haberKategoriId = x.haberKategoriId,
                haberTarih = x.haberTarih,
                haberOkuma = x.haberOkuma,
                haberUyeId = x.haberUyeId,
            }).ToList();

            return liste;
        }
        [HttpGet]
        [Route("api/haberbyid/{haberId}")]
        public HaberModel HaberById(int HaberId)
        {
            HaberModel kayit = db.Haber.Where(s => s.haberId == HaberId).Select(x => new HaberModel()
            {
                haberId = x.haberId,
                haberBaslik = x.haberBaslik,
                haberDetay = x.haberDetay,
                haberKategoriId = x.haberKategoriId,
                haberTarih = x.haberTarih,
                haberOkuma = x.haberOkuma,
                haberUyeId = x.haberUyeId,
            }).SingleOrDefault();
            return kayit;
        }
        [HttpPost]
        [Route("api/haberekle")]
        public SonucModel HaberEkle(HaberModel model)
        {
            if (db.Haber.Count(s => s.haberBaslik == model.haberBaslik) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Haber Başlığı Kayıtlıdır!";
                return sonuc;
            }
            Haber yeni = new Haber();
            yeni.haberBaslik = model.haberBaslik;
            yeni.haberDetay = model.haberDetay;
            yeni.haberTarih = model.haberTarih;
            yeni.haberOkuma = model.haberOkuma;
            yeni.haberKategoriId = model.haberKategoriId;
            yeni.haberUyeId = model.haberUyeId;
            db.Haber.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Haber Eklendi";

            return sonuc;
        }
        [HttpPut]
        [Route("api/haberduzenle")]
        public SonucModel HaberDuzenle(HaberModel model)
        {
            Haber kayit = db.Haber.Where(s => s.haberId == model.haberId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!"; return sonuc;
            }
            kayit.haberBaslik = model.haberBaslik;
            kayit.haberDetay = model.haberDetay;
            kayit.haberTarih = model.haberTarih;
            kayit.haberOkuma = model.haberOkuma;
            kayit.haberKategoriId = model.haberKategoriId;
            kayit.haberUyeId = model.haberUyeId;

            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Haber Düzenlendi";

            return sonuc;

        }

        [HttpDelete]
        [Route("api/habersil/{haberId}")]
        public SonucModel haberSil(int haberId)
        {
            Haber kayit = db.Haber.Where(s => s.haberUyeId == haberId).SingleOrDefault(); if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";

                return sonuc;
            }

            if (db.Yorum.Count(s => s.yorumHaberId == haberId) > 0) {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Yorum Kayıtlı Haber Silinemez!";

                return sonuc;
            }

            db.Haber.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Haber Silindi";

            return sonuc;
        }

        #endregion
    }
}
