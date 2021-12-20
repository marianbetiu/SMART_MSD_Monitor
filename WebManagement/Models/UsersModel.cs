using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebManagement.Models
{
    public class UsersModel
    {
        public static ViewModels.Management.Users.UserIndexViewModel Authenticate(string identity)
        {
            ViewModels.Management.Users.UserIndexViewModel result = null;

            // only for test
            //identity = "CW01\\uidp2987";

            string loginName = string.Empty;
            string loginDomain = string.Empty;

            var identityChunks = identity.Split(new string[] { "\\" }, StringSplitOptions.None);

            if ((identityChunks != null) && (identityChunks.Count() >= 2))
            {
                loginDomain = identityChunks[0].Trim();
                loginName = identityChunks[1].Trim();

                
            }

            try
            {
                using (var model = Data.ModelAccess.GetNewDBModel())
                {
                    var entity = model.Users.FirstOrDefault(o => o.UserName == loginName);

                    if (entity != null)
                    {
                        result = LoadIndex(entity);
                    }
                    else
                        result = null;
                }
            }
            catch (Exception ex)
            {
                MultiPack.EventLogFileLib.Write(MultiPack.EventLogFileLib.Levels.ERROR, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString());
            }

            return result;
        }

        public static ViewModels.Management.Users.UserIndexViewModel LoadIndex(Data.User entity)
        {
            ViewModels.Management.Users.UserIndexViewModel result = null;

            if (entity != null)
            {
                result = new ViewModels.Management.Users.UserIndexViewModel
                {
                    Id = entity.UserId,
                    NameFull = entity.UserNameDescription,
                    NameLogin = entity.UserName
                };
            }

            return result;
        }

        public static List<ViewModels.Management.Users.UserIndexViewModel> QueryAllIndex()
        {
            var result = new List<ViewModels.Management.Users.UserIndexViewModel>();

            using(var model = Data.ModelAccess.GetNewDBModel())
            {
                var collection = model.Users.ToList();
                collection.ForEach(a => result.Add(LoadIndex(a)));
            }

            return result;
        }

        public static ViewModels.Management.Users.UserEditViewModel LoadEdit(Data.User entity)
        {
            ViewModels.Management.Users.UserEditViewModel result = null;

            if (entity != null)
            {
                result = new ViewModels.Management.Users.UserEditViewModel
                {
                    Id = entity.UserId,
                    NameFull = entity.UserNameDescription,
                    NameLogin = entity.UserName,
                    Password = entity.UserPassword
                };
            }

            return result;
        }

        public static ViewModels.Management.Users.UserEditViewModel QuerySingleEdit(int id)
        {
            ViewModels.Management.Users.UserEditViewModel result = null;

            try
            {
                using (var model = Data.ModelAccess.GetNewDBModel())
                {
                    var entity = model.Users.FirstOrDefault(o => o.UserId == id);

                    if (entity != null)
                    {
                        result = LoadEdit(entity);
                    }
                }
            }
            catch (Exception ex)
            {
                MultiPack.EventLogFileLib.Write(MultiPack.EventLogFileLib.Levels.ERROR, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString());
            }

            return result;
        }

        public static bool Update(ViewModels.Management.Users.UserEditViewModel viewModel)
        {
            bool result = false;

            try
            {
                if (viewModel != null)
                {
                    using (var model = Data.ModelAccess.GetNewDBModel())
                    {
                        var entityExisting = model.Users.FirstOrDefault(o => (o.UserId == viewModel.Id));

                        if (entityExisting == null)
                        {
                            entityExisting = new Data.User();

                            model.Users.Add(entityExisting);
                        }
                        entityExisting.UserName = viewModel.NameLogin;
                        entityExisting.UserNameDescription = viewModel.NameFull;

                        if (viewModel.Password != null && viewModel.Password != string.Empty)
                        {
                            entityExisting.UserPassword = viewModel.Password;
                        }
                        else
                        {
                            if (entityExisting.UserPassword == null)
                                entityExisting.UserPassword = string.Empty;
                        }
                        model.SaveChanges();

                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MultiPack.EventLogFileLib.Write(MultiPack.EventLogFileLib.Levels.ERROR, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString());
            }

            return result;
        }

        public static bool Delete(int id)
        {
            bool result = false;

            try
            {
                using (var model = Data.ModelAccess.GetNewDBModel())
                {
                    var entity = model.Users.FirstOrDefault(o => o.UserId == id);

                    if (entity != null)
                    {
                        model.Users.Remove(entity);
                    }

                    model.SaveChanges();

                    result = true;
                }
            }
            catch (Exception ex)
            {
                MultiPack.EventLogFileLib.Write(MultiPack.EventLogFileLib.Levels.ERROR, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString());
            }

            return result;
        }

    }
}

