using AutoMapper;
using FluentValidation;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Dtos.TableDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class TableService : ITableService
    {
        private readonly IGenericRepository<Table> _genericTableRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateTableDto> _createValidator;
        private readonly IValidator<UpdateTableDto> _updateValidator;
        private readonly ITableRepository _tableRepository;
        public TableService(IGenericRepository<Table> genericTableRepository, IMapper mapper, ITableRepository tableRepository, IValidator<CreateTableDto> createValidator, IValidator<UpdateTableDto> updateValidator)
        {
            _genericTableRepository = genericTableRepository;
            _mapper = mapper;
            _tableRepository = tableRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }


        public async Task<ResponseDto<List<ResultTableDto>>> GetAllTables()
        {
            try
            {
                var tables = await _genericTableRepository.GetAllAsync();
                if (tables.Count == 0)
                {
                    return new ResponseDto<List<ResultTableDto>>
                    {
                        Success = false,
                        Data = null,
                        Message = "Masalar bulunamadı",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<List<ResultTableDto>>(tables);
                return new ResponseDto<List<ResultTableDto>>
                {
                    Success = true,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultTableDto>>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir sorun oluştu",
                    ErrorCodes = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<DetailTableDto>> GetByIdTable(int id)
        {
            try
            {
                var table = await _genericTableRepository.GetByIdAsync(id);
                if (table == null)
                {
                    return new ResponseDto<DetailTableDto>
                    {
                        Success = false,
                        Data = null,
                        Message = "Masa bulunamadı",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<DetailTableDto>(table);
                return new ResponseDto<DetailTableDto>
                {
                    Success = true,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailTableDto>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir sorun oluştu",
                    ErrorCodes = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<DetailTableDto>> GetByTableNumber(int tableNumber)
        {
            try
            {
                var table = await _tableRepository.GetByTableNumberAsync(tableNumber);
                if (table == null)
                {
                    return new ResponseDto<DetailTableDto>
                    {
                        Success = false,
                        Data = null,
                        Message = "Masa bulunamadı",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<DetailTableDto>(table);
                return new ResponseDto<DetailTableDto>
                {
                    Success = true,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailTableDto>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir sorun oluştu",
                    ErrorCodes = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> AddTable(CreateTableDto dto)
        {
            try
            {
                var validate = await _createValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCodes = ErrorCodes.ValidationError,
                        Message = string.Join(" | ", validate.Errors.Select(e => e.ErrorMessage))
                    };
                }
                //var checkTable = await _genericTableRepository.GetByIdAsync(dto.TableNumber);
                //if (checkTable != null)
                //{
                //    return new ResponseDto<object>
                //    {
                //        Success = false,
                //        Data = null,
                //        Message = "Bu masa numarası zaten mevcut",
                //        ErrorCodes = ErrorCodes.DuplicateError
                //    };
                //}
                var result = _mapper.Map<Table>(dto);
                await _genericTableRepository.AddAsync(result);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Masa başarıyla eklendi"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir sorun oluştu",
                    ErrorCodes = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> UpdateTable(UpdateTableDto dto)
        {
            try
            {
                var validate = await _updateValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCodes = ErrorCodes.ValidationError,
                        Message = string.Join(" | ", validate.Errors.Select(e => e.ErrorMessage))
                    };
                }
              
                var table = await _genericTableRepository.GetByIdAsync(dto.Id);
                if (table is null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCodes = ErrorCodes.NotFound,
                        Message = "Masa bulunamadı"
                    };
                }
                var result = _mapper.Map(dto, table);

                await _genericTableRepository.UpdateAsync(result);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Masa güncellendi"
                };

            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir sorun oluştu",
                    ErrorCodes = ErrorCodes.Exception
                };
            }
        }
        public async Task<ResponseDto<object>> DeleteTable(int id)
        {
            try
            {
                var table = await _genericTableRepository.GetByIdAsync(id);
                if (table is null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCodes = ErrorCodes.NotFound,
                        Message = "Masa bulunamadı"
                    };
                }
                await _genericTableRepository.DeleteAsync(table);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Masa silindi"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir sorun oluştu",
                    ErrorCodes = ErrorCodes.Exception
                };
            }
        }
    }
}
