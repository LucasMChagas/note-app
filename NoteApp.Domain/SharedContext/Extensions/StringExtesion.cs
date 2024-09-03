﻿using System.Text;

namespace NoteApp.Domain.SharedContext.Extensions;

public static class StringExtesion
{
    public static string ToBase64(this string text)
       => Convert.ToBase64String(Encoding.ASCII.GetBytes(text));
}
