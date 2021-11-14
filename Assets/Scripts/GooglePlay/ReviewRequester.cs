using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Play.Review;

public class ReviewRequester : MonoBehaviour
{
    private const string LevelComplete = "LevelComplete";
    private int _levelComplete;
    private ReviewManager _reviewManager;
    private PlayReviewInfo _playReviewInfo;


    private void Start()
    {
        _levelComplete = PlayerPrefs.GetInt(LevelComplete, 0);

        if (_levelComplete >= 1)
            StartCoroutine(RequestInAppReview());
    }

    private IEnumerator RequestInAppReview()
    {
        _reviewManager = new ReviewManager();

        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        _playReviewInfo = requestFlowOperation.GetResult();

        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null; // Reset the object
        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        // The flow has finished. The API does not indicate whether the user
        // reviewed or not, or even whether the review dialog was shown. Thus, no
        // matter the result, we continue our app flow.
    }
}
