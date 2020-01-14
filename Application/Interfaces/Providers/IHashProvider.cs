using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Providers
{
    public interface IHashProvider
    {
        string Hash(string text);

        bool Verify(string text, string hash);
    }
}
