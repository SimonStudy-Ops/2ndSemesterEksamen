using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using Core.Models;

namespace WebApplication1.Data
{
    public static class DatabaseSeederSimple
    {
        public static void Seed(string connectionString = "mongodb://localhost:27017", string dbName = "basement")
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(dbName);

            var brugerColl = db.GetCollection<BsonDocument>("Brugers");
            var varerColl = db.GetCollection<BsonDocument>("Varer");
            var vbColl = db.GetCollection<BsonDocument>("VarerBeholdning");
            var kategoriColl = db.GetCollection<BsonDocument>("Kategorier");

            // Slet gamle testdata
            brugerColl.DeleteMany(Builders<BsonDocument>.Filter. Empty);
            varerColl.DeleteMany(Builders<BsonDocument>.Filter.Empty);
            vbColl.DeleteMany(Builders<BsonDocument>. Filter.Empty);
            kategoriColl.DeleteMany(Builders<BsonDocument>.Filter.Empty);

            // Brugere
            var brugerDocs = new List<BsonDocument>();
            if (DataStore. Brugere != null)
            {
                foreach (var b in DataStore.Brugere)
                {
                    var doc = new BsonDocument
                    {
                        { "_id", b.Brugerid },
                        // IKKE duplikeret Brugerid - _id er nok! 
                        { "Navn", b.Navn ?? string.Empty },
                        { "tlfnr", b.tlfnr ?? string.Empty },
                        { "Mail", b.Mail ?? string.Empty },
                        { "IsAdmin", b.IsAdmin },
                        { "Password", b.Password ?? string.Empty },
                        { "opretelse", b.opretelse.ToDateTime(new TimeOnly(0,0), DateTimeKind. Utc) }
                    };
                    brugerDocs.Add(doc);
                }
                if (brugerDocs.Count > 0) brugerColl.InsertMany(brugerDocs);
            }

            // Varer
            var varerDocs = new List<BsonDocument>();
            if (DataStore.Varer != null)
            {
                foreach (var v in DataStore.Varer)
                {
                    var doc = new BsonDocument
                    {
                        { "_id", v. Varerid },
                        // IKKE duplikeret Varerid - kun _id!
                        // MongoDB mapper _id til Varerid property automatisk
                        { "Navn", v.Navn ?? string. Empty },
                        { "Enhed", v.Enhed ?? string.Empty },
                        { "Udløbsdato", v.Udløbsdato.ToDateTime(new TimeOnly(0,0), DateTimeKind.Utc) },
                        { "Beskrivelse", v.Beskrivelse ??  string.Empty },
                        { "Billede", v. Billede ?? string.Empty },
                        { "Kategorier", new BsonDocument { { "kategoriNavn", v.Kategorier?. kategoriNavn ?? string.Empty } } }
                    };
                    varerDocs.Add(doc);
                }
                if (varerDocs.Count > 0) varerColl.InsertMany(varerDocs);
            }

            // VarerBeholdning
            var vbDocs = new List<BsonDocument>();
            if (DataStore.VarerBeholdning != null)
            {
                foreach (var vb in DataStore.VarerBeholdning)
                {
                    var lokalitet = vb.Lokalitet != null
                        ? new BsonDocument { { "LokationId", vb.Lokalitet.LokationId }, { "LokationNavn", vb. Lokalitet.LokationNavn ??  string.Empty } }
                        : new BsonDocument();

                    var doc = new BsonDocument
                    {
                        { "_id", vb. VarerbeholdId },
                        // IKKE duplikeret VarerbeholdId
                        { "Mængde", vb.Mængde },
                        { "Lokalitet", lokalitet },
                        { "VarerId", vb.VarerId },
                        { "VarerNavn", vb.VarerNavn ?? string.Empty }
                    };
                    vbDocs.Add(doc);
                }
                if (vbDocs.Count > 0) vbColl.InsertMany(vbDocs);
            }

            // Kategorier - som separate dokumenter
            var kategoriDocs = new List<BsonDocument>();
            if (DataStore. Kategorier != null)
            {
                foreach (var k in DataStore.Kategorier)
                {
                    var doc = new BsonDocument
                    {
                        { "_id", k.kategoriNavn ??  string.Empty }
                        // IKKE duplikeret kategoriNavn
                    };
                    kategoriDocs.Add(doc);
                }
                if (kategoriDocs.Count > 0) kategoriColl. InsertMany(kategoriDocs);
            }

            Console. WriteLine("Seed færdig (simpel).");
        }
    }
}