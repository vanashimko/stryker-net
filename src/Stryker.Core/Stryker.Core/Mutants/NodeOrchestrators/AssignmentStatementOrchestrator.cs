﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace Stryker.Core.Mutants.NodeOrchestrators
{
    internal class AssignmentStatementOrchestrator : ExpressionSpecificOrchestrator<AssignmentExpressionSyntax>
    {
        public AssignmentStatementOrchestrator(MutantOrchestrator mutantOrchestrator) : base(mutantOrchestrator)
        {
        }

        // mutations must be controlled at the statement level
        protected override MutationContext StoreMutations(AssignmentExpressionSyntax node,
            IEnumerable<Mutant> mutations,
            MutationContext context)
        {
            context.StatementLevelControlledMutations.AddRange(mutations);
            return context;
        }
    }
}