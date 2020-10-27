﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace Stryker.Core.Instrumentation
{
    class IfInstrumentationEngine : BaseEngine<IfStatementSyntax>
    {
        public IfInstrumentationEngine(string annotation) : base(annotation, "IfInstrumentation")
        {
        }

        public IfStatementSyntax InjectIf(ExpressionSyntax condition, StatementSyntax originalNode, StatementSyntax mutatedNode)
        {
            return SyntaxFactory.IfStatement(condition,
                AsBlock(mutatedNode),
                SyntaxFactory.ElseClause(AsBlock(originalNode))).WithAdditionalAnnotations(Marker);
        }

        private static BlockSyntax AsBlock(StatementSyntax code)
        {
            return (code as BlockSyntax) ?? SyntaxFactory.Block(code);
        }

        protected override SyntaxNode Revert(IfStatementSyntax ifNode)
        {
            if (ifNode.Else?.Statement is BlockSyntax block)
            {
                return block.Statements.Count == 1 ? block.Statements[0] : block;
            }
            throw new InvalidOperationException($"Expected a block containing an 'else' statement, found:\n{ifNode.ToFullString()}.");
        }
    }
}
