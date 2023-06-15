using static ExercicioWebAPI.Models.Entities.UserRoles;

namespace ExercicioWebAPI.Models.Entities
{
    public class Usuario : Base
    {
        public string Nome { get; set; }
        public UserRole Role{ get; set; }
    }
}
