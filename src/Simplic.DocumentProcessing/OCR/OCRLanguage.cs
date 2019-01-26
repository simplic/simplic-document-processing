using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing
{
    public enum OCRLanguage
    {
        //
        // Summary:
        //     Unknown or undefined language.
        Unknown = -1,
        //
        // Summary:
        //     Afrikaans - the code is afr.
        Afrikaans = 0,
        //
        // Summary:
        //     Amharic - the code is amh.
        Amharic = 1,
        //
        // Summary:
        //     Arabic - the code is ara.
        Arabic = 2,
        //
        // Summary:
        //     Assamese - the code is asm.
        Assamese = 3,
        //
        // Summary:
        //     Azerbaijani - the code is aze.
        Azerbaijani = 4,
        //
        // Summary:
        //     Azerbaijani - Cyrillic - the code is aze_cyrl.
        AzerbaijaniCyrillic = 5,
        //
        // Summary:
        //     Belarusian - the code is bel.
        Belarusian = 6,
        //
        // Summary:
        //     Bengali - the code is ben.
        Bengali = 7,
        //
        // Summary:
        //     Tibetan -the code is bod.
        Tibetan = 8,
        //
        // Summary:
        //     Bosnian - the code is bos.
        Bosnian = 9,
        //
        // Summary:
        //     Bulgarian - the code is bul.
        Bulgarian = 10,
        //
        // Summary:
        //     Catalan, Valencian - the code is cat.
        CatalanValencian = 11,
        //
        // Summary:
        //     Cebuano - the code is ceb.
        Cebuano = 12,
        //
        // Summary:
        //     Czech - the code is ces.
        Czech = 13,
        //
        // Summary:
        //     Chinese - Simplified - the code is chi_sim.
        ChineseSimplified = 14,
        //
        // Summary:
        //     Chinese - Traditional - the code is chi_tra.
        ChineseTraditional = 15,
        //
        // Summary:
        //     Cherokee - the code is chr.
        Cherokee = 16,
        //
        // Summary:
        //     Welsh - the code is cym.
        Welsh = 17,
        //
        // Summary:
        //     Danish - the code is dan.
        Danish = 18,
        //
        // Summary:
        //     German - the code is deu.
        German = 19,
        //
        // Summary:
        //     Dzongkha - the code is dzo.
        Dzongkha = 20,
        //
        // Summary:
        //     Greek (Modern (1453-)) - the code is ell.
        Greek = 21,
        //
        // Summary:
        //     English - the code is eng.
        English = 22,
        //
        // Summary:
        //     Esperanto - the code is epo.
        Esperanto = 23,
        //
        // Summary:
        //     Estonian - the code is est.
        Estonian = 24,
        //
        // Summary:
        //     Basque - the code is eus.
        Basque = 25,
        //
        // Summary:
        //     Persian - the code is fas.
        Persian = 26,
        //
        // Summary:
        //     Finnish - the code is fin.
        Finnish = 27,
        //
        // Summary:
        //     French - the code is fra.
        French = 28,
        //
        // Summary:
        //     Frankish - the code is frk.
        Frankish = 29,
        //
        // Summary:
        //     Irish - the code is gle.
        Irish = 30,
        //
        // Summary:
        //     Galician - the code is glg.
        Galician = 31,
        //
        // Summary:
        //     Gujarati - the code is guj.
        Gujarati = 32,
        //
        // Summary:
        //     Haitian, Haitian Creole - the code is hat.
        HaitianCreole = 33,
        //
        // Summary:
        //     Hebrew - the code is heb.
        Hebrew = 34,
        //
        // Summary:
        //     Hindi - the code is hin.
        Hindi = 35,
        //
        // Summary:
        //     Croatian - the code is hrv.
        Croatian = 36,
        //
        // Summary:
        //     Hungarian - the code is hun.
        Hungarian = 37,
        //
        // Summary:
        //     Inuktitut - the code is iku.
        Inuktitut = 38,
        //
        // Summary:
        //     Indonesian - the code is ind.
        Indonesian = 39,
        //
        // Summary:
        //     Icelandic - the code is isl.
        Icelandic = 40,
        //
        // Summary:
        //     Italian - the code is ita.
        Italian = 41,
        //
        // Summary:
        //     Italian Old- the code is ita_old.
        Italian_Old = 42,
        //
        // Summary:
        //     Javanese - the code is jav.
        Javanese = 43,
        //
        // Summary:
        //     Japanese - the code is jpn.
        Japanese = 44,
        //
        // Summary:
        //     Kannada - the code is kan.
        Kannada = 45,
        //
        // Summary:
        //     Georgian - the code is kat.
        Georgian = 46,
        //
        // Summary:
        //     Georgian Old- the code is kat_old.
        Georgian_Old = 47,
        //
        // Summary:
        //     Kazakh - the code is kaz.
        Kazakh = 48,
        //
        // Summary:
        //     Central Khmer - the code is khm.
        CentralKhmer = 49,
        //
        // Summary:
        //     Kirghiz, Kyrgyz - the code is kir.
        Kirghiz = 50,
        //
        // Summary:
        //     Korean - the code is kor.
        Korean = 51,
        //
        // Summary:
        //     Kurdish - the code is kur.
        Kurdish = 52,
        //
        // Summary:
        //     Lao - the code is lao.
        Lao = 53,
        //
        // Summary:
        //     Latin - the code is lat.
        Latin = 54,
        //
        // Summary:
        //     Latvian - the code is lav.
        Latvian = 55,
        //
        // Summary:
        //     Lithuanian - the code is lit.
        Lithuanian = 56,
        //
        // Summary:
        //     Malayalam - the code is mal.
        Malayalam = 57,
        //
        // Summary:
        //     Marathi - the code is mar.
        Marathi = 58,
        //
        // Summary:
        //     Macedonian - the code is mkd.
        Macedonian = 59,
        //
        // Summary:
        //     Maltese - the code is mlt.
        Maltese = 60,
        //
        // Summary:
        //     Malay - the code is msa.
        Malay = 61,
        //
        // Summary:
        //     Burmese - the code is mya.
        Burmese = 62,
        //
        // Summary:
        //     Nepali - the code is nep.
        Nepali = 63,
        //
        // Summary:
        //     Dutch, Flemish - the code is nld.
        Dutch = 64,
        //
        // Summary:
        //     Norwegian - the code is nor.
        Norwegian = 65,
        //
        // Summary:
        //     Oriya - the code is ori.
        Oriya = 66,
        //
        // Summary:
        //     Panjabi - the code is pan.
        Panjabi = 67,
        //
        // Summary:
        //     Polish - the code is pol.
        Polish = 68,
        //
        // Summary:
        //     Portuguese - the code is por.
        Portuguese = 69,
        //
        // Summary:
        //     Pushto, Pashto - the code is pus.
        Pushto = 70,
        //
        // Summary:
        //     Romanian, Moldavian, Moldovan - the code is ron.
        Romanian = 71,
        //
        // Summary:
        //     Russian - the code is rus.
        Russian = 72,
        //
        // Summary:
        //     Sanskrit - the code is san.
        Sanskrit = 73,
        //
        // Summary:
        //     Sinhala, Sinhalese - the code is sin.
        Sinhala = 74,
        //
        // Summary:
        //     Slovak - the code is slk.
        Slovak = 75,
        //
        // Summary:
        //     Slovenian - the code is slv.
        Slovenian = 76,
        //
        // Summary:
        //     Spanish, Castilian - the code is spa.
        Spanish = 77,
        //
        // Summary:
        //     Spanish, Castilian - the code is spa_old.
        Spanish_Old = 78,
        //
        // Summary:
        //     Albanian - the code is sqi.
        Albanian = 79,
        //
        // Summary:
        //     Serbian - the code is srp.
        Serbian = 80,
        //
        // Summary:
        //     Serbian - Latin - the code is srp-latn.
        SerbianLatin = 81,
        //
        // Summary:
        //     Swahili - the code is swa.
        Swahili = 82,
        //
        // Summary:
        //     Swedish - the code is swe.
        Swedish = 83,
        //
        // Summary:
        //     Syriac - the code is syr.
        Syriac = 84,
        //
        // Summary:
        //     Tamil - the code is tam.
        Tamil = 85,
        //
        // Summary:
        //     Telugu - the code is tel.
        Telugu = 86,
        //
        // Summary:
        //     Tajik - the code is tgk.
        Tajik = 87,
        //
        // Summary:
        //     Tagalog - the code is tgl.
        Tagalog = 88,
        //
        // Summary:
        //     Thai - the code is tha.
        Thai = 89,
        //
        // Summary:
        //     Tigrinya - the code is tir.
        Tigrinya = 90,
        //
        // Summary:
        //     Turkish - the code is tur.
        Turkish = 91,
        //
        // Summary:
        //     Uighur, Uyghur - the code is uig.
        Uighur = 92,
        //
        // Summary:
        //     Ukrainian - the code is ukr.
        Ukrainian = 93,
        //
        // Summary:
        //     Urdu - the code is urd.
        Urdu = 94,
        //
        // Summary:
        //     Uzbek - the code is uzb.
        Uzbek = 95,
        //
        // Summary:
        //     Uzbek - Cyrillic - the code is uzb-cyrl.
        UzbekCyrillic = 96,
        //
        // Summary:
        //     Vietnamese - the code is vie.
        Vietnamese = 97,
        //
        // Summary:
        //     Yiddish - the code is yid.
        Yiddish = 98
    }
}
