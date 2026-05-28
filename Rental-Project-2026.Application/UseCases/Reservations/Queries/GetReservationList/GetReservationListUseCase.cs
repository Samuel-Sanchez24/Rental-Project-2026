using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Reservations.Queries.GetReservationList
{
    public class GetReservationListUseCase : IRequestHandler<GetReservationListQuery, PaginationResponse<ReservationListItemDTO>>
    {
        private readonly IReservationsRepository _reservationsRepository;

        public GetReservationListUseCase(IReservationsRepository reservationsRepository)
        {
            _reservationsRepository = reservationsRepository;
        }

        public async Task<PaginationResponse<ReservationListItemDTO>> Handle(GetReservationListQuery query)
        {
            PaginationResponse<Reservation> pagedReservations = await _reservationsRepository.GetPagedList(
                query.Pagination,
                query.UserIdFilter,
                query.VehicleIdFilter,
                query.BranchIdFilter,
                query.StatusFilter,
                CancellationToken.None);

            List<ReservationListItemDTO> itemsDTO = pagedReservations.Items
                .Select(r => r.ToDTO())
                .ToList();

            return PaginationResponse<ReservationListItemDTO>.Create(
                itemsDTO,
                pagedReservations.TotalCount,
                query.Pagination);
        }
    }
}
