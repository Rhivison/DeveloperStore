using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class Branch
    {
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }

        public Branch() { }

        public Branch(Guid branchId, string branchName, string branchCode)
        {
            BranchId = branchId;
            BranchName = branchName ?? throw new ArgumentNullException(nameof(branchName));
            BranchCode = branchCode ?? throw new ArgumentNullException(nameof(branchCode));
        }
    }
}