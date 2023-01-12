using System;
using System.ComponentModel.DataAnnotations;

namespace Donum.Models;

public class Entity
{
	[Key] public Guid Id { get; set; }
}