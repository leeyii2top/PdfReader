using PdfXenon.Standard;
using System;
using System.Text;
using System.IO;
using Xunit;
using UnitTesting;

namespace TokenizerUnitTesting
{
    public class TokenizerKeyword : HelperMethods
    {
        [Fact]
        public void KeywordFail()
        {
            Tokenizer t = new Tokenizer(StringToStream("random"));
            TokenBase k = t.GetToken();
            Assert.NotNull(k);
            Assert.True(k is TokenError);
            Assert.True(k.Position == 0);
            Assert.True(t.GetToken() is TokenEmpty);
        }

        [Fact]
        public void KeywordTrue()
        {
            Tokenizer t = new Tokenizer(StringToStream("true"));
            TokenKeyword k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Position == 0);
            Assert.True(k.Keyword == PdfKeyword.True);
            Assert.True(t.GetToken() is TokenEmpty);
        }

        [Fact]
        public void KeywordTrueIncorrect()
        {
            Tokenizer t = new Tokenizer(StringToStream("True"));
            TokenError e = t.GetToken() as TokenError;
            Assert.NotNull(e);
            Assert.True(e.Position == 0);
            Assert.True(t.GetToken() is TokenEmpty);
        }

        [Fact]
        public void KeywordFalse()
        {
            Tokenizer t = new Tokenizer(StringToStream("false"));
            TokenKeyword k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Position == 0);
            Assert.True(k.Keyword == PdfKeyword.False);
            Assert.True(t.GetToken() is TokenEmpty);
        }

        [Fact]
        public void KeywordFalseIncorrect()
        {
            Tokenizer t = new Tokenizer(StringToStream("False"));
            TokenError e = t.GetToken() as TokenError;
            Assert.NotNull(e);
            Assert.True(e.Position == 0);
            Assert.True(t.GetToken() is TokenEmpty);
        }

        [Fact]
        public void KeywordNull()
        {
            Tokenizer t = new Tokenizer(StringToStream("null"));
            TokenKeyword k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Position == 0);
            Assert.True(k.Keyword == PdfKeyword.Null);
            Assert.True(t.GetToken() is TokenEmpty);
        }

        [Fact]
        public void KeywordNullIncorrect()
        {
            Tokenizer t = new Tokenizer(StringToStream("Null"));
            TokenError e = t.GetToken() as TokenError;
            Assert.NotNull(e);
            Assert.True(e.Position == 0);
            Assert.True(t.GetToken() is TokenEmpty);
        }

        [Fact]
        public void KeywordStream()
        {
            Tokenizer t = new Tokenizer(StringToStream("stream"));
            TokenKeyword k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Position == 0);
            Assert.True(k.Keyword == PdfKeyword.Stream);
            Assert.True(t.GetToken() is TokenEmpty);
        }

        [Fact]
        public void KeywordEndStream()
        {
            Tokenizer t = new Tokenizer(StringToStream("endstream"));
            TokenKeyword k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Position == 0);
            Assert.True(k.Keyword == PdfKeyword.EndStream);
            Assert.True(t.GetToken() is TokenEmpty);
        }

        [Fact]
        public void KeywordObj()
        {
            Tokenizer t = new Tokenizer(StringToStream("obj"));
            TokenKeyword k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Position == 0);
            Assert.True(k.Keyword == PdfKeyword.Obj);
            Assert.True(t.GetToken() is TokenEmpty);
        }

        [Fact]
        public void KeywordEndObj()
        {
            Tokenizer t = new Tokenizer(StringToStream("endobj"));
            TokenKeyword k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Position == 0);
            Assert.True(k.Keyword == PdfKeyword.EndObj);
            Assert.True(t.GetToken() is TokenEmpty);
        }

        [Fact]
        public void KeywordR()
        {
            Tokenizer t = new Tokenizer(StringToStream("R"));
            TokenKeyword k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Position == 0);
            Assert.True(k.Keyword == PdfKeyword.R);
            Assert.True(t.GetToken() is TokenEmpty);
        }

        [Fact]
        public void KeywordXRef()
        {
            Tokenizer t = new Tokenizer(StringToStream("xref"));
            TokenKeyword k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Position == 0);
            Assert.True(k.Keyword == PdfKeyword.XRef);
            Assert.True(t.GetToken() is TokenEmpty);
        }

        [Fact]
        public void KeywordTrailer()
        {
            Tokenizer t = new Tokenizer(StringToStream("trailer"));
            TokenKeyword k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Position == 0);
            Assert.True(k.Keyword == PdfKeyword.Trailer);
            Assert.True(t.GetToken() is TokenEmpty);
        }

        [Fact]
        public void KeywordStartXRef()
        {
            Tokenizer t = new Tokenizer(StringToStream("startxref"));
            TokenKeyword k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Position == 0);
            Assert.True(k.Keyword == PdfKeyword.StartXRef);
            Assert.True(t.GetToken() is TokenEmpty);
        }

        [Fact]
        public void KeywordMultiple()
        {
            Tokenizer t = new Tokenizer(StringToStream("true false true false null stream endstream " +
                                                       "obj endobj R xref trailer startxref"));

            TokenKeyword k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Position == 0);
            Assert.True(k.Keyword == PdfKeyword.True);

            k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Position == 5);
            Assert.True(k.Keyword == PdfKeyword.False);

            k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Position == 11);
            Assert.True(k.Keyword == PdfKeyword.True);

            k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Position == 16);
            Assert.True(k.Keyword == PdfKeyword.False);

            k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Keyword == PdfKeyword.Null);

            k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Keyword == PdfKeyword.Stream);

            k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Keyword == PdfKeyword.EndStream);

            k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Keyword == PdfKeyword.Obj);

            k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Keyword == PdfKeyword.EndObj);

            k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Keyword == PdfKeyword.R);

            k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Keyword == PdfKeyword.XRef);

            k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Keyword == PdfKeyword.Trailer);

            k = t.GetToken() as TokenKeyword;
            Assert.NotNull(k);
            Assert.True(k.Keyword == PdfKeyword.StartXRef);

            Assert.True(t.GetToken() is TokenEmpty);
        }
    }
}