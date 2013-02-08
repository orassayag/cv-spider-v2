﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Professions
{
    public Professions() { }

    public static string GetRandomProfession()
    {
        Random R = new Random();
        List<string> Professions = ProfessionsList();
        return Professions.ElementAt(R.Next(1, Professions.Count));
    }

    public static List<string> ProfessionsList()
    {
        return new List<string>()
        {
            "רכזת פרוייקטים",
            "רכז פרוייקטים",
            "מנהלת אדמיניסטרטיבית",
            "מנהל אדמיניסטרטיבי",
            "מנהל/ת תפעול",
            "מנהלת תפעול",
            "מנהל תפעול",
            "מנהלת רכש",
            "מנהל רכש",
            "אשת יחסי ציבור",
            "איש יחסי ציבור",
            "רכזת משאבי אנוש",
            "רכז משאבי אנוש",
            "מזכירה",
            "מנהלת משאבי אנוש",
            "מנהל משאבי אנוש",
            "פקידת",
            "פקיד",
            "פקידת קבלה",
            "פקיד קבלה",
            "מתאמת",
            "אדמיניסטרציה",
            "כוח אדם",
            "אחראית ניהול מלאי",
            "אחראי ניהול מלאי",
            "ניהול משרד",
            "מנהלת השתלמויות",
            "מנהל השתלמויות",
            "מנהלת צוות",
            "מנהל צוות",
            "ראש צוות",
            "פקידת גבייה",
            "פקיד גבייה",
            "פקידת גביה",
            "פקיד גביה",
            "פקידת ביטוח",
            "פקיד ביטוח",
            "אחראית ניהול",
            "אחראי ניהול",
            "אחראית רכש",
            "אחראי רכש",
            "פקידת רכש",
            "פקיד רכש",
            "מנהלת תפעול",
            "מנהל תפעול",
            "מנהלת הדרכה",
            "מנהלת חשבונות",
            "מנהלת משרד",
            "אחראית משרד",
            "פקידת משרד",
            "מנהלת שיווק",
            "אחראית שיווק",
            "פקידת שיווק",
            "ייבוא וייצוא",
            "מנהלת פרוייקטים",
            "אחראית משאבי אנוש",
            "פקידת עורך דין",
            "מזכירת עורך דין",
            "אחראית כספים",
            "מנהלת כספים",
            "מזכירות",
            "פקידת ייבוא",
            "פקידת ייצוא",
            "פקידת מחסן",
            "פקידת מחסן רכש",
            "רכזת עמדה",
            "פקידה במחלקת חשבונות",
            "פקידת דוקומנטים",
            "פקידה זמנית",
            @"מזכירת מנכ""ל",
            "מזכירה רפואית",
            "פקידת מוקד",
            "מנהלת מוקד",
            "מתאמת פגישות",
            "מתאמת מכירות",
            "פקידת שטח",
            "מזכירת ערב",
            "מזכירה למשרד",
            @"פקידה למזכירת מנכ""ל",
            "רכזת עמדה",
            "פקידת משלוחים",
            "אחראית תפעול",
            "מזכירת שיווק",
            "מזכיר שיווק",
            "מנהלת בק אופיס",
            "מנהל בק אופיס",
            "אחראית ייבוא",
            "אחראי ייבוא",
            "אחראית ייצוא",
            "אחראי ייצוא",
            "מוקדנית",
            "מוקדן",
            "מזכירת הנהלה",
            @"מזכירת משנה למנכ""ל",
            "עובדת אדמיניסטרציה",
            "עובד אדמיניסטרציה",
            "רפרנטית שירות",
            "רכזת משאבי אנוש",
            "רכז משאבי אנוש",
            "מזכירה לאגף שיקומי",
            "מתאמת אספקות",
            "מתאם אספקות",
            "מזכירה אישית",
            "מזכיר אישי",
            "מזכירת מטה",
            "מזכיר מטה",
            "מזכירת סניף",
            "מזכיר סניף",
            "מנהלת סניף",
            "מנהל סניף",
            "אחראית סניף",
            "אחראי סניף",
            "רכזת סניף",
            "רכז סניף"
        };
    }
}