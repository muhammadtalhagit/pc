using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Trainer
{
    public int TrainerId { get; set; }

    public string TrainerName { get; set; } = null!;

    public int TrainerPhone { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
