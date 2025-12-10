using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Core.Models;

namespace WebApplication1.Data
{
    public static class DatabaseSeederSimple
    {
        public static async Task SeedAsync(IMongoDatabase db)
        {
            var brugerColl = db.GetCollection<BsonDocument>("Brugers");
            var varerColl = db.GetCollection<BsonDocument>("Varer");
            var vbColl = db.GetCollection<BsonDocument>("VarerBeholdning");
            var kategoriColl = db.GetCollection<BsonDocument>("Kategorier");
            await brugerColl.DeleteManyAsync(Builders<BsonDocument>.Filter.Empty);
            await varerColl.DeleteManyAsync(Builders<BsonDocument>.Filter.Empty);
            await vbColl.DeleteManyAsync(Builders<BsonDocument>.Filter.Empty);
            await kategoriColl.DeleteManyAsync(Builders<BsonDocument>.Filter.Empty);
            var brugerDocs = new List<BsonDocument>();
            if (DataStore.Brugere != null)
            {
                foreach (var b in DataStore.Brugere)
                {
                    var doc = new BsonDocument
                    {
                        { "_id", b.Brugerid },
                        { "Navn", b.Navn ?? string.Empty },
                        { "tlfnr", b.tlfnr ?? string.Empty },
                        { "Mail", b.Mail ?? string.Empty },
                        { "IsAdmin", b.IsAdmin },
                        { "Password", b.Password ?? string.Empty },
                        { "opretelse", b.opretelse.ToDateTime(new TimeOnly(0,0), DateTimeKind.Utc) }
                    };
                    brugerDocs.Add(doc);
                }
                if (brugerDocs.Count > 0) await brugerColl.InsertManyAsync(brugerDocs);
            }
            var varerDocs = new List<BsonDocument>();
            if (DataStore.Varer != null)
            {
                foreach (var v in DataStore.Varer)
                {
                    var doc = new BsonDocument
                    {
                        { "_id", v.Varerid },
                        { "Navn", v.Navn ?? string.Empty },
                        { "Enhed", v.Enhed ?? string.Empty },
                        { "Beskrivelse", v.Beskrivelse ?? string.Empty },
                        { "Billede", v.Billede ?? string.Empty },
                        { "Kategorier", new BsonDocument { { "kategoriNavn", v.Kategorier?.kategoriNavn ?? string.Empty } } }
                    };
                    varerDocs.Add(doc);
                }
                if (varerDocs.Count > 0) await varerColl.InsertManyAsync(varerDocs);
            }
            var vbDocs = new List<BsonDocument>();
            if (DataStore.VarerBeholdning != null)
            {
                foreach (var vb in DataStore.VarerBeholdning)
                {
                    var lokalitet = vb.Lokalitet != null
                        ? new BsonDocument { { "LokationId", vb.Lokalitet.LokationId }, { "LokationNavn", vb.Lokalitet.LokationNavn ?? string.Empty } }
                        : new BsonDocument();
                    var doc = new BsonDocument
                    {
                        { "_id", vb.VarerbeholdId },
                        { "Mængde", vb.Mængde },
                        { "Lokalitet", lokalitet },
                        { "VarerId", vb.VarerId },
                        { "VarerNavn", vb.VarerNavn ?? string.Empty },
                        { "Udløbsdato", vb.Udløbsdato.ToDateTime(new TimeOnly(0,0), DateTimeKind.Utc) }
                    };
                    vbDocs.Add(doc);
                }
                if (vbDocs.Count > 0) await vbColl.InsertManyAsync(vbDocs);
            }
            var kategoriDocs = new List<BsonDocument>();
            if (DataStore.Kategorier != null)
            {
                foreach (var k in DataStore.Kategorier)
                {
                    var doc = new BsonDocument
                    {
                        { "_id", k.kategoriNavn ?? string.Empty }
                    };
                    kategoriDocs.Add(doc);
                }
                if (kategoriDocs.Count > 0) await kategoriColl.InsertManyAsync(kategoriDocs);
            }
            Console.WriteLine("Seed færdig (async).");
        }
    }
}