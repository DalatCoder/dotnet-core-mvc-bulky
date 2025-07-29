using System.Linq.Expressions;

namespace Bulky.DataAccess.Repository.IRepository;

public interface IRepository<T> where T : class
{
    // T có thể là Category hoặc bất kỳ generic model nào khác
    // để thực hiện các thao tác CRUD hoặc tương tác với DB context
    
    // Lấy tất cả bản ghi
    IEnumerable<T> GetAll(string? includeProperties = null);
    
    // Lấy một bản ghi dựa trên điều kiện LINQ
    T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
    
    // Thêm một entity
    void Add(T entity);
    
    // Xóa một entity
    void Remove(T entity);
    
    // Xóa nhiều entities cùng lúc
    void RemoveRange(IEnumerable<T> entities);
}