using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToSqlProject
{
    class Database
    {
        public static void ClearTable()
        {
            using (ScoreDbEntities db = new ScoreDbEntities())
            {
                var rows = from o in db.Scores
                           select o;
                foreach (var row in rows)
                {
                    db.Scores.Remove(row);
                }
                db.SaveChanges();
            }
        }
        public static void AddScore(string firstName, string lastName, string course, decimal score)
        {
            using(ScoreDbEntities db = new ScoreDbEntities())
            {
                var testScore = new Score();
                testScore.FirstName = firstName;
                testScore.LastName = lastName;
                testScore.Class = course;
                testScore.TestScore = score;

                db.Scores.Add(testScore);
                db.SaveChanges();
            }
        }
    }
}
