using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
public class FBManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonholder;
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(OnInitDone, onFBHide);
        }
        else
        {
            FB.ActivateApp();
        }
    }
    public void OnInitDone()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            Debug.Log("FB initialized");
        }
        else
        {
            Debug.Log("FB not initialized");
        }
    }

    public void onFBHide(bool onHide)
    {
        if (onHide)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Login()
    {
        if (FB.IsInitialized)
        {
            if (!FB.IsLoggedIn)
            {
                List<string> permissions = new List<string>() {"public_profile", "email"};
                FB.LogInWithReadPermissions(permissions, onFBloginDone);
            }
            else
            {
                Debug.Log("user is already logged in");
            }
        }
    }

    public void onFBloginDone(ILoginResult res)
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("logged in");
        }
        else
        {
            Debug.LogError("error logging in " + res.Error);
        }
    }

    public void Upload()
    {
        buttonholder.SetActive(false);
        if (FB.IsLoggedIn)
        {
            StartCoroutine(UploadPhotoRoutine());
        }
        else
        {
            Debug.Log("FB is not Logged in");
        }
        buttonholder.SetActive(true);
    }

    IEnumerator UploadPhotoRoutine()
    {
        yield return new WaitForEndOfFrame();
        Texture2D screen = ScreenCapture.CaptureScreenshotAsTexture();
        byte[] screenBytes = screen.EncodeToPNG();

        WWWForm form = new WWWForm();
        form.AddBinaryData("image", screenBytes, "screenshot.png");
        form.AddField("caption", "Score Screenshot");
        FB.API("me/photos", HttpMethod.POST, onUploadDone, form);

        Debug.Log("uploading image");
    }

    public void onUploadDone(IGraphResult res)
    {
        if (string.IsNullOrEmpty(res.Error))
        {
            Debug.Log("Upload Done");
        }
        else
        {
            Debug.LogError("Error: " + res.Error);
        }
    }
}
