﻿using Appointment.Application.DTO;
using PetShop.Core.Entities;
using PetShop.Domain.Entities.Enums;

namespace Appointment.Application.Services.Interfaces
{
    public interface IAppointmentsService
    {
        Task<Response<AppointmentsDto>> CreateAppointment(AppointmentsDto appointmentsDto);
        Task<Response<AppointmentsDto>> GetById(int id);
        Task<Response<PaginationResult<AppointmentsDto>>> GetByUser(int userId, int pageIndex, int pageSize);
        Task<Response<PaginationResult<AppointmentsDto>>> GetByStatus(StatusAppointments status, int pageIndex, int pageSize);
        Task<Response<AppointmentsDto>> GetByPet(int petId);
        Task<Response<PaginationResult<AppointmentsDto>>> GetAll(int pageIndex, int pageSize);
        Task<bool> DeleteAppointment(int id);
        Task<Response<AppointmentsDto>> UpdateService(int id, AppointmentsDto appointmentsDto);
        Task<bool> AddServiceAppointment(int appointmentId, List<int> servicesId);
    }
}
