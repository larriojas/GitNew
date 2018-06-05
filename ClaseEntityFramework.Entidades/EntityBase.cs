namespace ClaseEntityFramework.Entidades
{
    public class EntityBase
    {
        public EntityBase()
        {
            EstadoRegistro = true;
        }

        public bool EstadoRegistro { get; set; }
    }
}