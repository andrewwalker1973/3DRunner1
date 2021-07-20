using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpiningManager : MonoBehaviour
{

	int randVal;
	private float timeInterval;
	private bool isCoroutine;
	private int finalAngle;

	public TextMeshProUGUI winText;
	public TextMeshProUGUI spinButtonText;
	[SerializeField] TextMeshProUGUI TotalCoinScoreText;
	public Button spinButton;
	public int section;
	float totalAngle;
	public string[] PrizeName;

	public int TurnCost = 300;          // How much coins user waste when turn whe wheel
	public int PreviousCoinsAmount;     // For wasted coins animation
	public int CurrentCoinAmount;
	public TextMeshProUGUI CoinsDeltaText;         // Pop-up text with wasted or rewarded coins amount
	private int currentSpinnerCredits;



	private ScoreManager theScoreManager;       // reference the score manager

   // private void Awake()
 //   {
	//	PreviousCoinsAmount = theScoreManager.totalCoinScore;
	//}

    // Use this for initialization
    private void Start()
	{
		theScoreManager = FindObjectOfType<ScoreManager>();         // find the score Manager script
		CurrentCoinAmount = theScoreManager.totalCoinScore;
		PreviousCoinsAmount = theScoreManager.totalCoinScore;
		currentSpinnerCredits = theScoreManager.spinnerCredits;


		isCoroutine = true;
		totalAngle = 360 / section;
		winText.text = " ";
		spinButtonText.text = "Watch to Spin ";
		if (theScoreManager.spinnerCredits > 0)
        {
			spinButtonText.text = "Free Spins " + theScoreManager.spinnerCredits;
		}			


	}

    private void OnEnable()
    {
		winText.gameObject.SetActive(false);

	}


    public void SpinButton()
	{

		if (isCoroutine)
		{
			if (theScoreManager.spinnerCredits > 0)
            {
				PreviousCoinsAmount = CurrentCoinAmount;
				theScoreManager.spinnerCredits--;
				theScoreManager.SaveSpinnerCount(theScoreManager.spinnerCredits);
				StartCoroutine(Spin());
			}
			else
				{
				
				// AW -- need a check for playing vid

				//	PreviousCoinsAmount = CurrentCoinAmount;
					// Decrease money for the turn
				//	CurrentCoinAmount -= TurnCost;
					// Show wasted coins
				//	CoinsDeltaText.text = "-" + TurnCost;
				//	CoinsDeltaText.gameObject.SetActive(true);

					// Animate coins
					//StartCoroutine(HideCoinsDelta());
					//StartCoroutine(UpdateCoinsAmount());

					StartCoroutine(Spin());
				}
		}
	}
	private IEnumerator Spin()
	{


		

		spinButtonText.text = "Spinning";
		winText.gameObject.SetActive(false);

		spinButton.interactable = false;
		isCoroutine = false;
		randVal = Random.Range(20, 100);

		timeInterval = 0.0001f * Time.deltaTime * 2;

		for (int i = 0; i < randVal; i++)
		{

			transform.Rotate(0, 0, (totalAngle / 2)); //Start Rotate 


			//To slow Down Wheel
			if (i > Mathf.RoundToInt(randVal * 0.2f))
				timeInterval = 0.5f * Time.deltaTime;
			if (i > Mathf.RoundToInt(randVal * 0.5f))
				timeInterval = 1f * Time.deltaTime;
			if (i > Mathf.RoundToInt(randVal * 0.7f))
				timeInterval = 1.5f * Time.deltaTime;
			if (i > Mathf.RoundToInt(randVal * 0.8f))
				timeInterval = 2f * Time.deltaTime;
			if (i > Mathf.RoundToInt(randVal * 0.9f))
				timeInterval = 2.5f * Time.deltaTime;

			yield return new WaitForSeconds(timeInterval);

		}

		if (Mathf.RoundToInt(transform.eulerAngles.z) % totalAngle != 0) //when the indicator stop between 2 numbers,it will add aditional step 
			transform.Rotate(0, 0, totalAngle / 2);

		finalAngle = Mathf.RoundToInt(transform.eulerAngles.z);//round off euler angle of wheel value

		print(finalAngle);


		//Prize check
		for (int i = 0; i < section; i++)
		{

			if (finalAngle == i * totalAngle)
			{
				//winText.text = "You won  " + PrizeName [i];
			}



		}

		switch ((int)finalAngle)
		{
			case 0: // 0
				RewardCoins(1000);
				break;
			case 45:  // 1
				RewardCoins(200);
				break;
			case 90: // 2
				RewardGems(10);
				break;
			case 135: // 3
				RewardCoins(500);
				break;
			case 180: // 4
				RewardSpinner(1);
				break;
			case 225: //5
				RewardGems(1);
				break;
			case 270: //6
				RewardSpinner(1);
				break;
			case 315: //7
				RewardGems(2);
				break;

			default:
				RewardCoins(10);
				break;


		}


		isCoroutine = true;
		if (theScoreManager.spinnerCredits > 0)
		{
			spinButtonText.text = "Free Spins " + theScoreManager.spinnerCredits;
		}
		else
        {
			spinButtonText.text = "Watch to Spin ";
		}
		
		spinButton.interactable = true;
	}

	private void RewardCoins(int awardCoins)
	{

		winText.text = "You won " + awardCoins + " Coins";
		winText.gameObject.SetActive(true);
		theScoreManager.SpinnerAddCoins(awardCoins);
		theScoreManager.SaveTotalCoinCount();
		theScoreManager.UpdateCoinsTextUI();
		theScoreManager.SaveSpinnerCount(theScoreManager.spinnerCredits);
	}


	private void RewardGems(int awardGems)
    {
		winText.text = "You won " + awardGems + " Gems";
		winText.gameObject.SetActive(true);
		theScoreManager.SpinnerAddGems(awardGems);
		theScoreManager.SaveCrystalCount();
		theScoreManager.UpdateGemsTextUI();
		
	}

	private void RewardSpinner(int awardSpinner)
	{
		winText.text = "You won " + awardSpinner + " Spins";
		winText.gameObject.SetActive(true);
		theScoreManager.SpinnerAddSpins(awardSpinner);
		theScoreManager.SaveSpinnerCountTotal();


	}
	void UpdateCoinsTextUI()
	{
		TotalCoinScoreText.text = GameData.Coins.ToString();
	}

	private IEnumerator HideCoinsDelta()
	{
		yield return new WaitForSeconds(1f);
		CoinsDeltaText.gameObject.SetActive(false);
	}

	private IEnumerator UpdateCoinsAmount()
	{
		// Animation for increasing and decreasing of coins amount
		const float seconds = 0.5f;
		float elapsedTime = 0;

		while (elapsedTime < seconds)
		{
			TotalCoinScoreText.text = Mathf.Floor(Mathf.Lerp(PreviousCoinsAmount, CurrentCoinAmount, elapsedTime / seconds)).ToString();
			//Debug.Log("TotalCoinScoreText " + TotalCoinScoreText.text);
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		

		PreviousCoinsAmount = CurrentCoinAmount;
		TotalCoinScoreText.text = TotalCoinScoreText.ToString();
	}
}
