using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Porter.Domain;
using Porter.Domain.Interfaces;
using System.Linq.Expressions;
using Porter.Common.Utils.ExtensionMethods;


namespace Porter.Infra.Postgres.Repository.Repository
{
    public class BookingRepository : IBookingRepository
    {

        private readonly AppDbContext _context;
        private readonly ILogger<BookingRepository> _logger;

        public BookingRepository(AppDbContext context, ILogger<BookingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Booking>> GetAll()
        {
            try
            {
                var bookings = await _context.Bookings.Where(p => !p.DeletedDate.HasValue).ToListAsync();

                
                _logger.LogInformation($"Returned {bookings.Count} bookings.");


                return bookings;
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                _logger.LogError(ex, "Erro ao retornar reservas.");
                throw;
            }
        }

        public async Task<Booking?> GetById(int id)
        {
            try
            {

                var booking = await _context.Bookings.FindAsync(id);

                if(booking is not null && booking.DeletedDate.HasValue)
                    return null;

                return booking;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar uma reserva");
                throw;
            }
        }

        public async Task<int> GetBookingCountByRoomAndPeriod(int roomId, int? bookingId, DateTime startDate, DateTime endDate)
        {
            try
            {
                startDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
                endDate = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);


                Expression<Func<Booking, bool>> predicate = (p =>
                p.Room.Id == roomId
                &&
                (
                ((p.StartDate >= startDate && p.StartDate <= endDate)
                ||
                (p.EndDate >= startDate && p.EndDate <= endDate))

                ||
                ((startDate >= p.StartDate && startDate <= p.EndDate)
                ||
                (endDate >= p.StartDate && endDate <= p.EndDate)))

                && (!p.DeletedDate.HasValue));

                if(bookingId.HasValue)
                    predicate = predicate.AndAlso(p => p.Id != bookingId.Value);

                var total = await _context.Bookings.CountAsync(predicate);

                return total;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar uma sala");
                throw;
            }
        }


        public async Task<IList<Booking>> GetBookingListByRoomAndPeriod(int roomId, DateTime startDate, DateTime endDate)
        {
            try
            {

                var list = await _context.Bookings.Where(p =>
                p.Room.Id == roomId
               &&
                (
                ((p.StartDate >= startDate && p.StartDate <= endDate)
                ||
                (p.EndDate >= startDate && p.EndDate <= endDate))

                ||
                ((startDate >= p.StartDate && startDate <= p.EndDate)
                ||
                (endDate >= p.StartDate && endDate <= p.EndDate)))
                && (!p.DeletedDate.HasValue))
                .ToListAsync();


                return list;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar uma sala");
                throw;
            }
        }

        public async Task<int> Register(Booking booking)
        {
            try
            {
                _context.Bookings.Add(booking);
                int bookingId = await _context.SaveChangesAsync();

                if (bookingId > 0)
                    _logger.LogInformation($"Reserva {booking.Id} salvo com sucesso!");
                else
                    _logger.LogInformation($"Erro ao cadastrar uma reserva!");


                return booking.Id;
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                _logger.LogError(ex, "Erro ao gravar uma reserva.");
                throw;
            }
        }

        public async Task<int> Delete(int Id)
        {
            try
            {

                var updatedRows = await _context.Bookings.Where(e => e.Id == Id && !e.DeletedDate.HasValue)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(e => e.DeletedDate, DateTime.UtcNow));

                //_context.Bookings.Update(booking);

                //int updatedRows = await _context.SaveChangesAsync();

                if (updatedRows > 0)
                    _logger.LogInformation($"Reserva {Id} excluida com sucesso!");
                else
                    _logger.LogInformation($"Reserva {Id} não encontrada!");

                return updatedRows;
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                _logger.LogError(ex, "Erro ao excluir uma reserva.");
                throw;
            }
        }


        public async Task<int> Update(Booking bookingToUpdate)
        {
            try
            {
                //_context.Bookings.Update(bookingToUpdate);

                //int updatedRows = await _context.SaveChangesAsync();


                var updatedRows = await _context.Bookings.Where(e => e.Id == bookingToUpdate.Id && !e.DeletedDate.HasValue)
                    .ExecuteUpdateAsync(setters =>
                     setters.SetProperty(e => e.StartDate, bookingToUpdate.StartDate)
                    .SetProperty(e => e.EndDate, bookingToUpdate.EndDate)
                    .SetProperty(e => e.Obs, bookingToUpdate.Obs)
                    //.SetProperty(e => e.ReservedBy, bookingToUpdate.ReservedBy)
                    //.SetProperty(e => e.ReservedById, bookingToUpdate.ReservedById)
                    );


                if (updatedRows > 0)
                    _logger.LogInformation($"Reserva {bookingToUpdate.Id} atualizada com sucesso!");
                else
                    _logger.LogInformation($"Reserva {bookingToUpdate.Id} não encontrada!");

                return updatedRows;
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                _logger.LogError(ex, "Erro ao excluir uma reserva.");
                throw;
            }
        }

    }
}
