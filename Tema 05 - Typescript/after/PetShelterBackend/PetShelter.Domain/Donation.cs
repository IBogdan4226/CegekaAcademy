using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.Domain;
public class Donation
{
    public decimal Amount { get; set; }
    public int DonorId { get; set; }
    public Person Donor { get; set; }
}
