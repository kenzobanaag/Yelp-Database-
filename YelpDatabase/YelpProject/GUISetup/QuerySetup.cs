using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YelpProject.GUISetup
{
    class QuerySetup
    {
        private SQL.SQLQueries newQuery = new SQL.SQLQueries();
        private SQL.IllegalInput inputChk = new SQL.IllegalInput();
        private SQL.SQLConstants sq = new SQL.SQLConstants();
        private Classes.ClassConstants c = new Classes.ClassConstants();
        private Dictionary<string, List<string>> query;
        private Classes.User newUser;
        private Classes.Business newBusiness;
        private Classes.Business currBusiness;
        private Map.MapDriver map = new Map.MapDriver();

        public List<Classes.User> GetUser(String username)
        {
            var userList = new List<Classes.User>();
            query = new Dictionary<string, List<string>>();
            query.Add(sq.SQL_PROJECT, new List<string>() { sq.SQL_SELECT, c.U_id });
            query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_FROM,c.U_table });
            query.Add(sq.SQL_WHERE, new List<string>() { c.U_name });
            query.Add(sq.SQL_COND, new List<string>() { username });
            List<Object[]> newObject = newQuery.SQLSelectQuery(query);
            foreach(var item in newObject)
            {
                var tempUser = new Classes.User()
                {
                    user_Id = item[0].ToString()
                };
                userList.Add(tempUser);
            }
            return userList;
        }

        public List<Classes.Business> GetBusiness(String BUsername)
        {
            query = new Dictionary<string, List<string>>();
            query.Add(sq.SQL_PROJECT, new List<string>() { sq.SQL_SELECT, c.B_id });
            query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_FROM,c.B_table });
            query.Add(sq.SQL_WHERE, new List<string>() { c.B_name });
            query.Add(sq.SQL_COND, new List<string>() { BUsername });
            List<Object[]> newObject = newQuery.SQLSelectQuery(query);
            var businessList = new List<Classes.Business>();
            foreach (var item in newObject)
            {
                var tempBusiness = new Classes.Business()
                {
                    business_id = item[0].ToString()
                };
                businessList.Add(tempBusiness);
            }
            return businessList;
        }

        public Classes.Business GetBusinessDetails(String businessID)
        {
            query = new Dictionary<string, List<string>>();
            query.Add(sq.SQL_PROJECT, new List<string>() { sq.SQL_SELECT, c.B_id, c.B_name, c.B_stars,
            c.B_city, c.B_zipcode, c.B_address, c.B_latitude, c.B_longitude, c.B_reviewCount, c.B_reviewRating
            ,c.B_numCheckins, c.B_state});
            query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_FROM, c.B_table });
            query.Add(sq.SQL_WHERE, new List<string>() { c.B_id });
            query.Add(sq.SQL_COND, new List<string>() { businessID });
            List<Object[]> newObject = newQuery.SQLSelectQuery(query);
            var tempBusiness = new Classes.Business() {
                business_id = newObject[0][0].ToString(),
                name = newObject[0][1].ToString(),
                stars = Double.Parse(newObject[0][2].ToString()),
                city = newObject[0][3].ToString(),
                zipcode = newObject[0][4].ToString(),
                address = newObject[0][5].ToString(),
                latitute = Double.Parse(newObject[0][6].ToString()),
                longtitude = Double.Parse(newObject[0][7].ToString()),
                review_count = Int32.Parse(newObject[0][8].ToString()),
                review_rating = Double.Parse(newObject[0][9].ToString()),
                num_checkins = Int32.Parse(newObject[0][10].ToString()),
                state = newObject[0][11].ToString()
            };
           
            return tempBusiness;
        }

        public Classes.User GetUserDetails(String userID)
        {
            query = new Dictionary<string, List<string>>();
            query.Add(sq.SQL_PROJECT, new List<string>() { sq.SQL_SELECT,c.U_id, c.U_name, c.U_avgStars, c.U_fans, c.U_yelpingSince, c.U_funny,
                c.U_cool, c.U_useful, c.U_latitude, c.U_longitude});
            query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_FROM,c.U_table });
            query.Add(sq.SQL_WHERE, new List<string>() { c.U_id });
            query.Add(sq.SQL_COND, new List<string>() { userID });
            List<Object[]> newObject = newQuery.SQLSelectQuery(query);
            foreach(Object[] users in newObject)
            {
                var tempUser = new Classes.User()
                {
                    user_Id = users[0].ToString(),
                    username = users[1].ToString(),
                    average_stars = Double.Parse(users[2].ToString()),
                    fans = Int32.Parse(users[3].ToString()),
                    yelping_since = users[4].ToString(),
                    funny = Int32.Parse(users[5].ToString()),
                    cool = Int32.Parse(users[6].ToString()),
                    useful = Int32.Parse(users[7].ToString()),
                    user_latitude = Double.Parse(users[8].ToString()),
                    user_longitude = Double.Parse(users[9].ToString())
                };
                return tempUser;
            }
            return null;
        }

        public void SetCurrentUser(Classes.User user)
        {
            newUser = user;
        }

        public void SetCurrentBusiness(Classes.Business business)
        {
            newBusiness = business;
        }

        public Classes.Business GetCurrentBusiness()
        {
            return newBusiness;
        }

        public void SetLoggedInBusiness(Classes.Business business)
        {
            currBusiness = business;
        }

        public Classes.Business GetLoggedInBusiness()
        {
            return currBusiness;
        }

        public List<Classes.User> GetFriends()
        {
            if (newUser != null)
            {
                query = new Dictionary<string, List<string>>();
                query.Add(sq.SQL_PROJECT, new List<String>() { sq.SQL_SELECT, c.FR_id });
                query.Add(sq.SQL_FROM, new List<String>() { sq.SQL_FROM, c.FR_table });
                query.Add(sq.SQL_WHERE, new List<String>() { c.FR_uid });
                query.Add(sq.SQL_COND, new List<String>() { newUser.user_Id });
                List<Object[]> newObject = newQuery.SQLSelectQuery(query);
                var userList = new List<Classes.User>();
                foreach (Object[] item in newObject)
                {
                    var tempUser = new Classes.User()
                    {
                        user_Id = item[0].ToString()
                    };
                    userList.Add(tempUser);
                }
                return userList;
            }
            return null;
        }

        public List<Object[]> GetFriendReview(List<Classes.User> tempUserList)
        {
            List<Object[]> newObject = GetReviewByDate(tempUserList);
            var tempSolutionList = new List<object[]>();
            foreach (Object[] temp in newObject)
            {
                query = new Dictionary<string, List<string>>();
                query.Add(sq.SQL_PROJECT, new List<string>() { sq.SQL_SELECTDIST, c.B_name, c.B_city, c.R_text, c.R_date });
                query.Add(sq.SQL_FROM, new List<String>() { sq.SQL_FROM,c.B_table, c.R_table });
                query.Add(sq.SQL_WHERE, new List<string>() { c.R_date, c.R_user });
                query.Add(sq.SQL_COND, new List<string>() { temp[0].ToString(), temp[1].ToString() });
                List<Object[]> secondObjectQuery = newQuery.SQLSelectQuery(query);
                foreach(var item in secondObjectQuery)
                {
                    // Temporary solution as of now. 
                    var newTempA = GetUserDetails(temp[1].ToString()).username;
                    var newTempB = item[0];
                    var newTempC = item[1];
                    var newTempD = item[2];
                    var newTempE = item[3];
                    Object[] tempArray = new Object[] { newTempA, newTempB, newTempC, newTempD, newTempE };
                    tempSolutionList.Add(tempArray);
                } 
            }
            return tempSolutionList;
        }

        public List<Object[]> GetReviewByDate(List<Classes.User> tempUserList)
        {
            List<Object[]> newObject = new List<Object[]>();
            foreach (Classes.User temp in tempUserList)
            {
                query = new Dictionary<string, List<string>>();
                query.Add(sq.SQL_PROJECT, new List<string>() { sq.SQL_SELECTDIST, c.R_date, c.R_user });
                query.Add(sq.SQL_FROM, new List<String>() { sq.SQL_FROM, c.B_table, c.R_table });
                query.Add(sq.SQL_WHERE, new List<string>() { c.U_id });
                query.Add(sq.SQL_COND, new List<string>() { temp.user_Id });
                query.Add(sq.SQL_ORDER, new List<string>() { sq.SQL_ORDER, sq.SQL_DESC, c.R_date });
                //review logic, only adding first object? temp[0] is one user_id
                List<Object[]> reviewDate = newQuery.SQLSelectQuery(query);
                if(reviewDate != null && reviewDate.Count > 0)
                    newObject.Add(reviewDate[0]);
            }
            return newObject;
        }

        public List<Object[]> GetStates()
        {
            query = new Dictionary<string, List<string>>();
            query.Add(sq.SQL_PROJECT, new List<string>() { sq.SQL_SELECTDIST, c.B_state });
            query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_FROM,c.B_table });
            query.Add(sq.SQL_ORDER, new List<string>() { sq.SQL_ORDER, ";", c.B_state });
            List<Object[]> newObject = newQuery.SQLSelectQuery(query);
            return newObject;
        }

        public List<Object[]> GetCities(String state)
        {
            query = new Dictionary<string, List<string>>();
            query.Add(sq.SQL_PROJECT, new List<string>() { sq.SQL_SELECTDIST, c.B_city });
            query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_FROM,c.B_table });
            query.Add(sq.SQL_WHERE, new List<string>() { c.B_state });
            query.Add(sq.SQL_COND, new List<string>() {state });
            query.Add(sq.SQL_ORDER, new List<string>() { sq.SQL_ORDER, ";", c.B_city });
            List<Object[]> newObject = newQuery.SQLSelectQuery(query);
            return newObject;
        }

        public List<Object[]> GetZipcodes(List<string> location)
        {
            query = new Dictionary<string, List<string>>();
            query.Add(sq.SQL_PROJECT, new List<string>() { sq.SQL_SELECTDIST, c.B_zipcode });
            query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_FROM,c.B_table });
            query.Add(sq.SQL_WHERE, new List<string>() { c.B_state, c.B_city });
            query.Add(sq.SQL_COND, new List<string>() { location[0], location[1] });
            query.Add(sq.SQL_ORDER, new List<string>() { sq.SQL_ORDER, ";", c.B_zipcode });
            List<Object[]> newObject = newQuery.SQLSelectQuery(query);
            return newObject;
        }

        public List<Classes.Categories> GetCategories(List<string> location)
        {
            query = new Dictionary<string, List<string>>();
            query.Add(sq.SQL_PROJECT, new List<string>() { sq.SQL_SELECTDIST, c.CAT_name });
            query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_FROM,c.B_table, c.CAT_table });
            query.Add(sq.SQL_WHERE, new List<string>() { c.B_state, c.B_city, c.B_zipcode });
            query.Add(sq.SQL_COND, new List<string>() { location[0], location[1], location[2] });
            query.Add(sq.SQL_ORDER, new List<string>() { sq.SQL_ORDER, ";", c.CAT_name });
            List<Object[]> newObject = newQuery.SQLSelectQuery(query);
            List<Classes.Categories> catList = new List<Classes.Categories>();
            foreach (Object[] item in newObject)
            {
                Classes.Categories tempCat = new Classes.Categories()
                {
                    category_name = item[0].ToString()
                };
                catList.Add(tempCat);
            }
            return catList;
        }

        public List<Classes.Business> GetBusinesses(List<Classes.Attributes> tempAttr, List<string> location, List<string> catArray, List<string> order)
        {
            query = new Dictionary<string, List<string>>();
            List<String> test = new List<String>();
            List<String> test2 = new List<String>();
            List<Classes.Business> tempBusinessList = new List<Classes.Business>();
            query.Add(sq.SQL_PROJECT, new List<string>() { sq.SQL_SELECTDIST, c.SPEC_id,c.B_name, c.B_address, c.B_city, c.B_state,
            c.B_latitude, c.B_longitude, c.B_stars, c.B_reviewCount, c.B_reviewRating, c.B_numCheckins});
            if(tempAttr.Count > 0)
            {
                foreach(Classes.Attributes attr in tempAttr)
                {
                    test.Add(attr.attribute_name);
                    test2.Add(attr.attribute_value);
                }
                query.Add(sq.SQL_ATTR, test);
                query.Add(sq.SQL_VAL, test2);
            }
            if(catArray.Count > 0) 
                query.Add(sq.SQL_CAT, catArray);
            query.Add(sq.SQL_WHERE, new List<string>() { c.B_state, c.B_city, c.B_zipcode });
            query.Add(sq.SQL_COND, new List<string>() { location[0], location[1], location[2] });
            String origOrder = order[0];
            order[0] = order[0].Equals(c.ORD_distance) ? order[0] = c.ORD_name : order[0];
            query.Add(sq.SQL_ORDER, new List<string>() { sq.SQL_ORDER, order[1], order[0] });
            List<Object[]> newObject = newQuery.SQLSelectQuery(query);
            for(int i = 0; i < newObject.Count; i++)
            {
                Classes.Business tempBusiness = new Classes.Business()
                {
                    business_id = newObject[i][0].ToString(),
                    name = newObject[i][1].ToString(),
                    address = newObject[i][2].ToString(),
                    city = newObject[i][3].ToString(),
                    state = newObject[i][4].ToString(),
                    latitute = Double.Parse(newObject[i][5].ToString()),
                    longtitude = Double.Parse(newObject[i][6].ToString()),
                    stars = Double.Parse(newObject[i][7].ToString()),
                    review_count = Int32.Parse(newObject[i][8].ToString()),
                    review_rating = Double.Parse(newObject[i][9].ToString()),
                    num_checkins = Int32.Parse(newObject[i][10].ToString()),
                    business_distance = "0"
                };
                if (newUser != null)
                {
                    map.CalculateDistance(new Microsoft.Maps.MapControl.WPF.Location(tempBusiness.latitute, tempBusiness.longtitude),
                    new Microsoft.Maps.MapControl.WPF.Location(newUser.user_latitude, newUser.user_longitude));
                    double dist = map.GetDistance();
                    tempBusiness.business_distance = dist.ToString(".## ");
                }
                tempBusinessList.Add(tempBusiness);
            }
            if(newUser != null && origOrder.Equals(c.ORD_distance))
            {
                tempBusinessList.Sort(delegate (Classes.Business x, Classes.Business y) {
                    return x.business_distance.CompareTo(y.business_distance);
                }); 
            }
            return tempBusinessList;
        }

        public int UpdateUserLocation(List<double> location)
        {
            if(newUser != null)
            {
                query = new Dictionary<string, List<string>>();
                query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_UPDATE, c.U_table });
                query.Add(sq.SQL_SET, new List<string>() { sq.SQL_SET, c.U_latitude, c.U_longitude });
                query.Add(sq.SQL_VAL, new List<string>() { "I",location[0].ToString(), location[1].ToString() });
                query.Add(sq.SQL_WHERE, new List<string>() { sq.SQL_WHERE, c.U_id });
                query.Add(sq.SQL_COND, new List<string>() { newUser.user_Id });
                return newQuery.SQLNonQuery(query);
            }
            return 0;
        }

        public List<Classes.Categories> GetCurrentBusinessCategories(String businessID)
        {
            query = new Dictionary<string, List<string>>();
            query.Add(sq.SQL_PROJECT, new List<string>() { sq.SQL_SELECT, c.CAT_name });
            query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_FROM, c.CAT_table });
            query.Add(sq.SQL_WHERE, new List<string>() { c.B_id});
            query.Add(sq.SQL_COND, new List<string>() { businessID });
            query.Add(sq.SQL_ORDER, new List<string>() { sq.SQL_ORDER, ";", c.CAT_name });
            List<Object[]> newObject = newQuery.SQLSelectQuery(query);
            var catList = new List<Classes.Categories>();
            foreach (var item in newObject)
            {
                var tempCat = new Classes.Categories()
                {
                    category_name = item[0].ToString()
                };
                catList.Add(tempCat);
            }
            return catList;
        }

        public List<Classes.Attributes> GetCurrentBusinessAttributes(String businessID)
        {
            query = new Dictionary<string, List<string>>();
            query.Add(sq.SQL_PROJECT, new List<string>() { sq.SQL_SELECT, c.ATTR_name, c.ATTR_val });
            query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_FROM, c.ATTR_table });
            query.Add(sq.SQL_WHERE, new List<string>() { c.B_id });
            query.Add(sq.SQL_COND, new List<string>() { businessID });
            query.Add(sq.SQL_ORDER, new List<string>() { sq.SQL_ORDER, ";", c.ATTR_name });
            List<Object[]> newObject = newQuery.SQLSelectQuery(query);
            var attrList = new List<Classes.Attributes>();
            foreach (var item in newObject)
            {
                var tempCat = new Classes.Attributes()
                {
                    attribute_name = item[0].ToString(),
                    attribute_value = item[1].ToString()
                };
                attrList.Add(tempCat);
            }
            return attrList;
        }

        public Classes.Hours GetCurrentBusinessHours(String businessID, String day)
        {
            query = new Dictionary<string, List<string>>();
            query.Add(sq.SQL_PROJECT, new List<string>() { sq.SQL_SELECT, c.HRS_day, c.HRS_open, c.HRS_close });
            query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_FROM, c.HRS_table });
            query.Add(sq.SQL_WHERE, new List<string>() { c.B_id, c.HRS_day });
            query.Add(sq.SQL_COND, new List<string>() { businessID, day });
            query.Add(sq.SQL_ORDER, new List<string>() { sq.SQL_ORDER, ";", c.HRS_day });
            List<Object[]> newObject = newQuery.SQLSelectQuery(query);
            if(newObject.Count > 0)
            {
                var newHours = new Classes.Hours()
                {
                    hours_day = newObject[0][0].ToString(),
                    hours_open = DateTime.Parse(newObject[0][1].ToString()),
                    hours_close = DateTime.Parse(newObject[0][2].ToString())
                };
                return newHours;
            }
            return null;
        }

        public void InsertCheckins(Classes.Business business)
        {
            DateTime rightNow = DateTime.Now;
            query = new Dictionary<string, List<string>>();
            query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_UPDATE, c.CHK_table });
            query.Add(sq.SQL_SET, new List<string>() { sq.SQL_SET, c.CHK_count });
            query.Add(sq.SQL_VAL, new List<string>() { "I",c.CHK_count +" + 1"   });
            query.Add(sq.SQL_WHERE, new List<string>() { sq.SQL_WHERE, c.CHK_day, c.CHK_time, c.B_id });
            query.Add(sq.SQL_COND, new List<string>() { rightNow.ToString("dddd"), rightNow.ToString("hh:mm"), business.business_id });
            int updated = newQuery.SQLNonQuery(query);
            //Does not exist in table so do an insert. 
            if (updated == 0)
            {
                query = new Dictionary<string, List<string>>();
                query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_INSERT, c.CHK_table });
                query.Add(sq.SQL_VAL, new List<string>() { sq.SQL_VAL, rightNow.ToString("dddd"),
                    rightNow.ToString("hh:mm"), "1", business.business_id });
                int newUpdate = newQuery.SQLNonQuery(query);
                ShowStatusUpdate(newUpdate, "Check In");
            }
            else
            {
                ShowStatusUpdate(updated, "Check In");
            }
        }
     

        public List<Classes.Review> GetBusinessReviews(Classes.Business business)
        {
            var reviewList = new List<Classes.Review>();
            query = new Dictionary<string, List<string>>();
            query.Add(sq.SQL_PROJECT, new List<string>() { sq.SQL_SELECT, c.R_date, c.R_reviewStars,
                c.R_text, c.R_funny, c.R_useful, c.R_cool, c.R_user });
            query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_FROM, c.R_table });
            query.Add(sq.SQL_WHERE, new List<string>() { c.B_id });
            query.Add(sq.SQL_COND, new List<string>() { business.business_id });
            query.Add(sq.SQL_ORDER, new List<string>() { sq.SQL_ORDER, sq.SQL_DESC, c.R_date });
            List<Object[]> newObject = newQuery.SQLSelectQuery(query);
            foreach(Object[] review in newObject)
            {
                var newReview = new Classes.Review()
                {
                    review_date = review[0].ToString(),
                    review_stars = Int32.Parse(review[1].ToString()),
                    review_text = review[2].ToString(),
                    funny_vote = Int32.Parse(review[3].ToString()),
                    useful_vote = Int32.Parse(review[4].ToString()),
                    cool_vote = Int32.Parse(review[5].ToString()),
                    review_username = GetUserDetails(review[6].ToString()).username
                };
                reviewList.Add(newReview);
            }
            return reviewList;
        }

        public void InsertReview(String reviewText, Classes.Business currBuss, String rating)
        {
            if(newUser != null)
            {
                if(reviewText.Length > 0)
                {
                    DateTime rightNow = DateTime.Now;
                    query = new Dictionary<string, List<string>>();
                    String reviewID = new StringHasher.SimpleStringHasher().GetHash();
                    query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_INSERT, c.R_table });
                    query.Add(sq.SQL_VAL, new List<string>() { sq.SQL_VAL, reviewID, currBuss.business_id, newUser.user_Id,
                    rating, rightNow.ToString("MM/dd/yyyy"), reviewText, "0", "0", "0" });
                    int updated = newQuery.SQLNonQuery(query);
                    ShowStatusUpdate(updated, "Add Review");
                }
                else
                    ShowStatusUpdate(0, "Please add text, Add Review");
            }
        }

        public int CheckFavoritesBeforeAdd()
        {
            int status = 0;
            if(newUser != null && newBusiness != null)
            {
                query = new Dictionary<string, List<string>>();
                query.Add(sq.SQL_PROJECT, new List<string>() { sq.SQL_SELECT, c.FAV_uid });
                query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_FROM, c.FAV_table });
                query.Add(sq.SQL_WHERE, new List<string>() { c.FAV_uid, c.FAV_bid });
                query.Add(sq.SQL_COND, new List<string>() { newUser.user_Id, newBusiness.business_id });
                List<Object[]> newObject = newQuery.SQLSelectQuery(query);
                status = newObject != null ? newObject.Count : 0;
            }
            return status;
        }

        public void InsertToFavorites(Classes.Business currBuss)
        {
            if(newUser != null)
            {
                query = new Dictionary<string, List<string>>();
                query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_INSERT, c.FAV_table });
                query.Add(sq.SQL_VAL, new List<string>() { sq.SQL_VAL, newUser.user_Id , currBuss.business_id});
                int updated = newQuery.SQLNonQuery(query);
                ShowStatusUpdate(updated, "Add to Favorites");
            }
        }

        public List<Classes.Business> GetUserFavorites()
        {
            if(newUser != null)
            {
                query = new Dictionary<string, List<string>>();
                query.Add(sq.SQL_PROJECT, new List<string>() { sq.SQL_SELECT, c.FAV_bid });
                query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_FROM, c.FAV_table });
                query.Add(sq.SQL_WHERE, new List<String>() { c.FAV_uid });
                query.Add(sq.SQL_COND, new List<String>() { newUser.user_Id });
                var newObject = newQuery.SQLSelectQuery(query);
                var businessList = new List<Classes.Business>();
                foreach(var item in newObject)
                {
                    var tempBusiness = new Classes.Business()
                    {
                        business_id = item[0].ToString()
                    };
                    businessList.Add(tempBusiness);
                }
                return businessList;
            }
            return null;
        }

        public int RemoveBusiness(String businessID)
        {
            query = new Dictionary<string, List<string>>();
            query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_DELETE, c.FAV_table });
            query.Add(sq.SQL_WHERE, new List<string>() { sq.SQL_WHERE, c.B_id });
            query.Add(sq.SQL_COND, new List<string>() { businessID });
            int updated = newQuery.SQLNonQuery(query);
            ShowStatusUpdate(updated, "Remove Business");
            return updated;
        }

        public Classes.Review GetBusinessFriendReviews(String friendID)
        {
            if(newUser != null)
            {
                List<Classes.Review> reviewList = new List<Classes.Review>();
                query = new Dictionary<string, List<string>>();
                query.Add(sq.SQL_PROJECT, new List<string>() { sq.SQL_SELECT, c.R_date, c.R_reviewStars,
                    c.R_text, c.R_funny, c.R_useful, c.R_cool, c.R_user });
                query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_FROM, c.R_table });
                query.Add(sq.SQL_WHERE, new List<string>() { c.B_id, c.U_id });
                query.Add(sq.SQL_COND, new List<string>() { newBusiness.business_id, friendID });
                query.Add(sq.SQL_ORDER, new List<string>() { sq.SQL_ORDER, sq.SQL_DESC, c.R_date });
                List<Object[]> newObject = newQuery.SQLSelectQuery(query);
                foreach (var review in newObject)
                {
                    if (review != null)
                    {
                        Classes.Review newReview = new Classes.Review()
                        {
                            review_date = review[0].ToString(),
                            review_stars = Int32.Parse(review[1].ToString()),
                            review_text = review[2].ToString(),
                            funny_vote = Int32.Parse(review[3].ToString()),
                            useful_vote = Int32.Parse(review[4].ToString()),
                            cool_vote = Int32.Parse(review[5].ToString()),
                            review_username = GetUserDetails(review[6].ToString()).username,
                            actual_review_date = review[0].ToString()
                        };
                        return newReview;
                    }
                }
            }
            return null;
        }

        public List<Classes.Checkins> GetBusinessCheckinDays(Classes.Business business)
        {
            query = new Dictionary<string, List<string>>();
            query.Add(sq.SQL_PROJECT, new List<string>() { sq.SQL_SELECTDIST, c.CHK_day });
            query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_FROM, c.CHK_table });
            query.Add(sq.SQL_WHERE, new List<string>() { c.B_id });
            query.Add(sq.SQL_COND, new List<string>() { business.business_id });
            query.Add(sq.SQL_ORDER, new List<string>() { sq.SQL_ORDER, ";", c.CHK_day });
            List<Object[]> newObject = newQuery.SQLSelectQuery(query);
            var checkinList = new List<Classes.Checkins>();
            foreach(var item in newObject)
            {
                var newCheckin = new Classes.Checkins()
                {
                    checkin_day = item[0].ToString()
                };
                checkinList.Add(newCheckin);
            }
            return checkinList;
        }

        public Classes.Checkins GetBusinessCheckins(Classes.Business business, String checkinsDay)
        {
            query = new Dictionary<string, List<string>>();
            query.Add(sq.SQL_PROJECT, new List<string>() { sq.SQL_SELECT, "SUM(" + c.CHK_count + ")" });
            query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_FROM, c.CHK_table });
            query.Add(sq.SQL_WHERE, new List<string>() { c.B_id, c.CHK_day});
            query.Add(sq.SQL_COND, new List<string>() { business.business_id , checkinsDay});
            List<Object[]> newObject = newQuery.SQLSelectQuery(query);
            if(newObject != null && newObject.Count > 0)
            {
                return new Classes.Checkins() {
                    checkin_day = checkinsDay,
                    checkin_count = Int32.Parse(newObject[0][0].ToString())
                };
            }
            return null;
        }
        
        public void ShowStatusUpdate(int status, String statText)
        {
            var newDialog = new DialogSetup(status, statText);
        }

        public List<string> GetOrder(int index)
        {
            String orderby = "";
            String order = "";
            switch (index)
            {
                case 0:
                    orderby = c.ORD_name;
                    order = sq.SQL_ASC;
                    break;
                case 1:
                    orderby = c.ORD_stars;
                    order = sq.SQL_DESC;
                    break;
                case 2:
                    orderby = c.ORD_reviwed;
                    order = sq.SQL_DESC;
                    break;
                case 3:
                    orderby = c.ORD_rating;
                    order = sq.SQL_DESC;
                    break;
                case 4:
                    orderby = c.ORD_checkin;
                    order = sq.SQL_DESC;
                    break;
                case 5:
                    orderby = c.ORD_distance;
                    order = sq.SQL_ASC;
                    break;
            }
            return new List<string>() { orderby, order };
        }

        public List<string> GetPhotoPaths(String businessID)
        {
            query = new Dictionary<string, List<string>>();
            query.Add(sq.SQL_PROJECT, new List<string>() { sq.SQL_SELECT, c.PIC_path });
            query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_FROM, c.PIC_table });
            query.Add(sq.SQL_WHERE, new List<string>() { c.B_id });
            query.Add(sq.SQL_COND, new List<string>() { businessID });
            List<Object[]> newObject = newQuery.SQLSelectQuery(query);
            var pathList = new List<string>();
            foreach(var item in newObject)
            {
                pathList.Add(item[0].ToString());
            }
            return pathList;
        }

        public void AddPhotoPath(String path, String businessID)
        {
            query = new Dictionary<string, List<string>>();
            query.Add(sq.SQL_PROJECT, new List<string>() { sq.SQL_SELECT, c.PIC_path });
            query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_FROM, c.PIC_table });
            query.Add(sq.SQL_WHERE, new List<string>() { c.B_id, c.PIC_path });
            query.Add(sq.SQL_COND, new List<string>() { businessID, path });
            List<Object[]> newObject = newQuery.SQLSelectQuery(query);
            if (newObject.Count == 0)
            {
                query = new Dictionary<string, List<string>>();
                query.Add(sq.SQL_FROM, new List<string>() { sq.SQL_INSERT, c.PIC_table });
                query.Add(sq.SQL_VAL, new List<string>() { sq.SQL_VAL, businessID, path });
                int updated = newQuery.SQLNonQuery(query);
                ShowStatusUpdate(updated, "Add Photo");
            }
            else
                ShowStatusUpdate(0, "Photo already exists, Add Photo");
        }

        
    }
}
