﻿using LeaveManagementSystem.Data;
using LeaveManagementSystem.Models;

namespace LeaveManagementSystem.Services
{
    public interface ILeaveValidationStrategyFactory
    {
        ILeaveValidationStrategy GetStrategy(LeaveType leaveType);
    }

    public class LeaveValidationStrategyFactory : ILeaveValidationStrategyFactory
    {
        private readonly ApplicationDbContext _context;

        public LeaveValidationStrategyFactory(ApplicationDbContext context)
        {
            _context = context;
        }

        public ILeaveValidationStrategy GetStrategy(LeaveType leaveType)
        {
            return leaveType switch
            {
                LeaveType.Annual => new AnnualLeaveValidationStrategy(_context),
                LeaveType.Sick => new SickLeaveValidationStrategy(),
                _ => throw new NotImplementedException($"Validation for {leaveType} is not implemented."),
            };
        }
    }

}
