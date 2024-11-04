using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Analytics;

public class UGS_Analytics : MonoBehaviour
{
    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            GiveConsent(); // Get user consent according to various legislations
        }
        catch (ConsentCheckException e)
        {
            Debug.Log(e.ToString());
        }
    }

    public void CatEvent(int catsEntered, int ordersDelivered, int ordersFailed) 
    {

        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { "cats_entered",  catsEntered},
            { "orders_eaten", ordersDelivered},
            { "orders_failed", ordersFailed},
        };

        AnalyticsService.Instance.CustomData("CatEvent", parameters);

        AnalyticsService.Instance.Flush();

    }

    public void CookEvent(int cooked_times)
    {

        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
                       { "cooked_times",  cooked_times},
        };

        AnalyticsService.Instance.CustomData("CookEvent", parameters);

        AnalyticsService.Instance.Flush();

    }

    public void CutEvent(int octopus_cut, int salmon_cut, int shrimp_cut)
    {

        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
                       { "octopus_cut",  octopus_cut},
                       { "salmon_cut",  salmon_cut},
                       { "shrimp_cut",  shrimp_cut},
        };


        AnalyticsService.Instance.CustomData("CutEvent", parameters);

        AnalyticsService.Instance.Flush();

    }

    public void PlateEvent(int cleaned_plates, int dirty_plates)
    {

        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
                       { "cleaned_plates",  cleaned_plates},
                       { "dirty_plates",  dirty_plates},
        };

        AnalyticsService.Instance.CustomData("PlateEvent", parameters);

        AnalyticsService.Instance.Flush();

    }

    public void PutPlaceEvent(int placed_pickables,int picked_pickables)
    {

        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
                       { "placed_pickables",  placed_pickables},
                       { "picked_pickables",  picked_pickables},
        };

        AnalyticsService.Instance.CustomData("PutPlaceEvent", parameters);

        AnalyticsService.Instance.Flush();

    }

    public void GiveConsent()
    {
        // Call if consent has been given by the user
        AnalyticsService.Instance.StartDataCollection();
        Debug.Log($"Consent has been provided. The SDK is now collecting data!");
    }
}
