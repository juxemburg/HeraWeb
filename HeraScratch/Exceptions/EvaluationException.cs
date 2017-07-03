﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HeraScratch.Exceptions
{
    public class EvaluationException : Exception
    {
        public EvaluationException()
        {

        }

        public EvaluationException(string message)
            :base(message)
        {

        }

        public EvaluationException(string message, Exception inner)
            :base(message, inner)
        {

        }

    }
}
