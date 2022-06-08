using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class WindowAdExample : MonoBehaviour
{
	[SerializeField] private WidgetAdsButton _buttonRunWidgetAd;
	[SerializeField] private Text _textLog;
	[SerializeField] private RotatingObject _rotatingObject;

	private void OnEnable()
	{

		AdController.Instance.OnAdLoadStartedEvent += OnAdLoadStartedEvent;
		AdController.Instance.OnAdShowCompleteEvent += OnAdShowCompleteEvent;
	}

	private void OnDisable()
	{

		AdController.Instance.OnAdLoadStartedEvent -= OnAdLoadStartedEvent;
		AdController.Instance.OnAdShowCompleteEvent -= OnAdShowCompleteEvent;
	}
	
	private void OnAdShowCompleteEvent()
	{
		_textLog.text = "Ad Showing complete";
		
	}

	private void OnAdLoadStartedEvent() { }

}
