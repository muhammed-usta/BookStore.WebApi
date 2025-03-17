using BookStore.BusinessLayer.Abstract;
using BookStore.DataAccessLayer.Abstract;
using BookStore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BusinessLayer.Concrete
{
    public class SubscriberManager : ISubscriberService
    {
        private readonly ISubscriberDal _subscriberDal;

        public SubscriberManager(ISubscriberDal subscriberDal)
        {
            _subscriberDal = subscriberDal;
        }

        public void TAdd(Subscriber entity)
        {
          _subscriberDal.Add(entity);
        }

        public void TDelete(int id)
        {
           _subscriberDal.Delete(id);
        }

        public List<Subscriber> TGetAll()
        {
          return _subscriberDal.GetAll();
        }

        public Subscriber TGetById(int id)
        {
          return  _subscriberDal.GetById(id);
        }

        public void TUpdate(Subscriber entity)
        {
            _subscriberDal.Update(entity);
        }
    }
}
