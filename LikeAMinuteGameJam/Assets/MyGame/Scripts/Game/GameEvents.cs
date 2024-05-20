using System;
using UnityEngine;

public class GameEvents
{
	public static Action OnExitSafeZone;

	public static Action ResetPlayer;

	public static Action<int> TotemPorta1;
	public static Action<int> Talisma;
	public static Action<int> Platform;

	public static Action<int> TimeCoin;

	public static Action FragmentMemory;
}
