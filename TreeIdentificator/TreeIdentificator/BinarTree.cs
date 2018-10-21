using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeIdentificator
{
    public class BinarTree
    {
        public BinarTree left;              //private
        public BinarTree right;             //private
        public Identificator Ident;

        public BinarTree()
        {
            left = null;
            right = null;
        }

        public BinarTree(Identificator ident)
        {
            Ident = ident;
        }

        public void AddTree(BinarTree tree)
        {
            if (tree.Ident.Hash > Ident.Hash)
            {
                if (right == null)
                    right = tree;
                else
                {
                    right.AddTree(tree);
                }
            }
            else
            {
                if (left == null)
                    left = tree;
                else
                {
                    left.AddTree(tree);
                }
            }
        }
    }
}
