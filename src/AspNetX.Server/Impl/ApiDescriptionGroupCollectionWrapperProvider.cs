﻿using AspNetX.Server.Abstractions;
using AspNetX.Server.Models;
using Microsoft.AspNet.Mvc.ApiExplorer;

namespace AspNetX.Server.Impl
{
    public class ApiDescriptionGroupCollectionWrapperProvider : IApiDescriptionGroupCollectionWrapperProvider
    {
        private readonly IApiDescriptionGroupCollectionProvider _descriptionProvider;

        public ApiDescriptionGroupCollectionWrapperProvider(IApiDescriptionGroupCollectionProvider descriptionProvider)
        {
            this._descriptionProvider = descriptionProvider;
        }

        public ApiDescriptionGroupCollectionWrapper ApiXDescriptionGroups
        {
            get { return new ApiDescriptionGroupCollectionWrapper(_descriptionProvider.ApiDescriptionGroups); }
        }
    }
}
