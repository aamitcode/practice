using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    public partial class Solution
    {
        public IList<IList<int>> ThreeSum(int[] nums)
        {

            //[-1,0,1,2,-1,-4]
            //[-1,0,0,1]
            IList<IList<int>> ret = new List<IList<int>>();
            Array.Sort(nums);
            for(int i=0;i< nums.Length - 2; i++)
            {
                if(i>0 && nums[i] == nums[i - 1])
                {
                    continue;
                }
                int target = 0 - nums[i];
                int j = i + 1;
                int k = nums.Length - 1;
                while (j < k)
                {
                    if(j>i+1 && nums[j] == nums[j - 1])
                    {
                        j++;
                        continue;
                    }
                    if(k< nums.Length-1 && nums[k] == nums[k + 1])
                    {
                        k--;
                        continue;
                    }
                    if (nums[j] + nums[k] == target)
                    {
                        List<int> temp = new List<int>();
                        temp.Add(nums[i]);
                        temp.Add(nums[j]);
                        temp.Add(nums[k]);
                        j++;
                        ret.Add(temp);
                    }
                    else
                    {
                        if(target < nums[j] + nums[k])
                        {
                            k--;
                        }
                        else
                        {
                            j++;
                        }
                    }

                }
            }
            return ret;
        }
    }
}
