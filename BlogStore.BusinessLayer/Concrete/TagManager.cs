using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogStore.BusinessLayer.Abstract;
using BlogStore.DataAccessLayer.Abstract;
using BlogStore.EntityLayer.Entities;

namespace BlogStore.BusinessLayer.Concrete
{
    public class TagManager : ITagService
    {
        private readonly ITagDal _ITagDal;

        public TagManager(ITagDal ıTagDal)
        {
            _ITagDal = ıTagDal;
        }

        public void TDelete(int id)
        {
            _ITagDal.Delete(id);
        }

        public List<Tag> TGetAll()
        {
          return _ITagDal.GetAll();
        }

        public Tag TGetById(int id)
        {
           return (_ITagDal.GetById(id));
        }

        public void TInsert(Tag entity)
        {
            _ITagDal.Insert(entity);
        }

        public void TUpdate(Tag entity)
        {
            _ITagDal.Update(entity);
        }
    }
}
