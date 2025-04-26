using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graficoU3D
{
    class CentroDeMasa
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public CentroDeMasa(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
        }

        public override string ToString()
        {
            return $"Centro de Masa: X = {X}, Y = {Y}, Z = {Z}";
        }
    }
}
