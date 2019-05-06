﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PdfXenon.Standard
{
    public class PdfContentsParser : PdfObject
    {
        private int _index = 0;
        private Parser _parser;
        private List<PdfStream> _streams = new List<PdfStream>();

        public PdfContentsParser(PdfObject parent, List<PdfStream> streams)
            : base(parent)
        {
            _streams = streams;
        }    

        public PdfObject GetObject()
        {
            // First time around we setup the parser to the first stream
            if ((_parser == null) && (_index < _streams.Count))
                _parser = new Parser(new MemoryStream(_streams[_index++].ValueAsBytes), true);

            // Keep trying to get a parsed object as long as there is a parser for a stream
            while (_parser != null)
            {
                ParseObject obj = _parser.ParseObject(true);
                if (obj != null)
                    return WrapObject(obj);

                _parser.Dispose();
                _parser = null;

                // Is there another stream we can continue parsing with
                if (_index < _streams.Count)
                    _parser = new Parser(new MemoryStream(_streams[_index++].ValueAsBytes), true);
            }

            return null;
        }
    }
}
