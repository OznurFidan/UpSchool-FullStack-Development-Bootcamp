using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSchool.Domain.Dtos
{
    public class Mementopassword
    {
        public List<string> mementopaswords;

        Stack<string> mementopasword = new Stack<string>();
        public Mementopassword()
        {
                
        }
        //public Mementopassword(string mementopassword)

        //{
        //    mementopaswords = new List<string>();


        //}
        public void MementoAddPaswords(string undopasword)
        {
            mementopasword.Push(undopasword);
        }

        public void MementoGoback()
        { 
            if (mementopasword.Count > 0) 
            {
               mementopaswords.Add( mementopasword.Pop());

            }
             
        }
      
        
        int mementocount = mementopaswords.Count;
       public void WriteUndoPassword(string undopassword)
        {
            undopassword = mementopaswords[mementocount];
        }

    }
}
