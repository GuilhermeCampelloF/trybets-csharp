using TryBets.Odds.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Globalization;

namespace TryBets.Odds.Repository;

public class OddRepository : IOddRepository
{
    protected readonly ITryBetsContext _context;
    public OddRepository(ITryBetsContext context)
    {
        _context = context;
    }

    public Match Patch(int MatchId, int TeamId, string BetValue)
    {
        string BetValueConverted = BetValue.Replace(',', '.');
        decimal BetValueDecimal = Decimal.Parse(BetValueConverted, CultureInfo.InvariantCulture);

        var findMatch = _context.Matches.FirstOrDefault(m => m.MatchId == MatchId);
        var findTeam = _context.Teams.FirstOrDefault(t => t.TeamId == TeamId);

        if (findMatch == null) {
            throw new Exception("Match not found");
        }
        if (findTeam == null) {
            throw new Exception("Team not found");
        }
        if (findMatch.MatchTeamAId != TeamId && findMatch.MatchTeamBId != TeamId) {
            throw new Exception($"Team {TeamId} not in this match");
        }
        if (findTeam.TeamId == findMatch.MatchTeamAId) {
            findMatch.MatchTeamAValue += BetValueDecimal;
        }
        else {
            findMatch.MatchTeamBValue += BetValueDecimal;
        }
        _context.SaveChanges();
        return findMatch;
    }
}