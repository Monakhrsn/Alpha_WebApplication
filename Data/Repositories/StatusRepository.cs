using Business.Models;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class StatusRepository(DataContext context) : BaseRepository<StatusEntity, Status>(context), IStatusRepository
{

}