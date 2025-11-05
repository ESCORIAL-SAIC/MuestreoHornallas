namespace MuestreoHornallas
{
    public static class Boton
    {
        public static class EncendidoElectronico
        {
            public static bool OK { get; set; } = false;
            public static bool NoOK { get; set; } = false;
        }
        public static class Voladura
        {
            public static bool Cero { get; set; } = false;
            public static bool Cuarto { get; set; } = false;
            public static bool Medio { get; set; } = false;
            public static bool Tercio { get; set; } = false;
            public static bool Entero { get; set; } = false;
        }
        public static class Retiene
        {
            public static bool SiRetiene { get; set; } = false;
            public static bool NoRetiene { get; set; } = false;
        }
    }
}
