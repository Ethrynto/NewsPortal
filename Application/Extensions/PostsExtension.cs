using Domain.Models;

namespace Application.Extensions;

public static class PostsExtension
{
    public static async Task<PageResult<Post>> ToPageAsync(this IQueryable<Post> query, PageParams param)
    {
        int count = query.Count();
        if (count < 1)
            return new PageResult<Post>([], 0);
        
        int page = param.Page ?? 1;
        int pageSize = param.PageSize ?? 10;
        int skip = (page - 1) * pageSize;
        
        var result = query.Skip(skip).Take(pageSize).ToArray();
        return new PageResult<Post>(result, count);
    }
}