using System;
using System.Collections.Generic;
using System.Text;

namespace Novetta.LearningProject.Ingress.Commands
{
    abstract class ACommand
    {
        public abstract bool Validate();
        public string Error { get; set; }
    }
}
