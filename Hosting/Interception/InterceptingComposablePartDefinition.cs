﻿namespace MefContrib.Hosting.Interception
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Primitives;
    using System.ComponentModel.Composition.ReflectionModel;

    /// <summary>
    /// Defines a <see cref="ComposablePartDefinition"/> which supports interception.
    /// </summary>
    public class InterceptingComposablePartDefinition : ComposablePartDefinition
    {
        private readonly IExportedValueInterceptor valueInterceptor;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterceptingComposablePartDefinition"/> class.
        /// </summary>
        /// <param name="interceptedPartDefinition">The <see cref="ComposablePartDefinition"/> being intercepted.</param>
        /// <param name="valueInterceptor">The <see cref="IExportedValueInterceptor"/> instance.</param>
        public InterceptingComposablePartDefinition(ComposablePartDefinition interceptedPartDefinition, IExportedValueInterceptor valueInterceptor)
        {
            if (interceptedPartDefinition == null) throw new ArgumentNullException("interceptedPartDefinition");
            if (valueInterceptor == null) throw new ArgumentNullException("valueInterceptor");
            
            InterceptedPartDefinition = interceptedPartDefinition;
            this.valueInterceptor = valueInterceptor;
        }

        /// <summary>
        /// Gets the intercepted <see cref="ComposablePartDefinition"/>.
        /// </summary>
        public ComposablePartDefinition InterceptedPartDefinition { get; private set; }

        public override ComposablePart CreatePart()
        {
            var interceptedPart = InterceptedPartDefinition.CreatePart();
            return ReflectionModelServices.IsDisposalRequired(InterceptedPartDefinition)
                       ? new DisposableInterceptingComposablePart(interceptedPart, this.valueInterceptor)
                       : new InterceptingComposablePart(interceptedPart, this.valueInterceptor);
        }

        public override IEnumerable<ExportDefinition> ExportDefinitions
        {
            get { return InterceptedPartDefinition.ExportDefinitions; }
        }

        public override IEnumerable<ImportDefinition> ImportDefinitions
        {
            get { return InterceptedPartDefinition.ImportDefinitions; }
        }

        public override IDictionary<string, object> Metadata
        {
            get { return InterceptedPartDefinition.Metadata; }
        }

        public override string ToString()
        {
            return InterceptedPartDefinition.ToString();
        }
    }
}
