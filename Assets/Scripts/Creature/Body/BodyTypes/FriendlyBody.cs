
public class FriendlyBody : Body {

	public void Feed( FoodEffect foodEffect ){

		if (foodEffect.Power != 0){
			Stats.AddBaseStats( StatsType.Power, foodEffect.Power );
		}

		if (foodEffect.Magick != 0){
			Stats.AddBaseStats( StatsType.Magick, foodEffect.Magick );
		}

		if (foodEffect.Defence != 0){
			Stats.AddBaseStats( StatsType.Defence, foodEffect.Defence );
		}

		if (foodEffect.MagickDefence != 0){
			Stats.AddBaseStats( StatsType.MagickDefence, foodEffect.MagickDefence );
		}

		if (foodEffect.Speed != 0){
			Stats.AddBaseStats( StatsType.Speed, foodEffect.Speed );
		}

		if (foodEffect.Luck != 0){
			Stats.AddBaseStats( StatsType.Luck, foodEffect.Luck );
		}
	}
}
