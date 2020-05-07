/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

namespace Common.Utils
{
    public static class Mapper
    {
        public static TResult MapTo<TResult>(this object source)
            where TResult : class
        {
            return AutoMapper.Mapper.Map<TResult>(source);
        }

        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return AutoMapper.Mapper.Map<TSource, TDestination>(source, destination);
        }
    }
}
