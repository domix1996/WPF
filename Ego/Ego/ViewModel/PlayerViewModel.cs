using System.Collections.Generic;
using System.Windows.Documents;

namespace Ego.ViewModel
{
    using Ego.Model;
    public class PlayerViewModel
    {
        public PlayerModel Me { get; set; }
        public List<PlayerModel> Others { get; set; }
    }
}