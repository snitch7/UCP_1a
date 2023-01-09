using UnityEngine;


public class PlayerShipBuild : MonoBehaviour {
    [SerializeField] private GameObject[] shopButtons;
    private GameObject target;
    private GameObject tmpSelection;
    private GameObject textBoxPanel;
    [SerializeField] private GameObject[] visualWeapons;
    [SerializeField] private SOActorModel defaultPlayerShip;
    private GameObject playerShip;
    public GameObject buyButton;
    private GameObject bankObj;
    private int bank = 600;
    private bool purchaseMade = false;

    private void Start() {
        TurnOffSelectionHighlights();
        textBoxPanel = GameObject.Find("textBoxPanel");
        purchaseMade = false;
        bankObj = GameObject.Find("bank");
        bankObj.GetComponentInChildren<TextMesh>().text = bank.ToString();
        buyButton = textBoxPanel.transform.Find("BUY ?").gameObject;

        TurnOffPlayerShipVisuals();
        PreparePlayerShipForUpgrade();
    }


    private void WatchAdvert() {
        print("watching ad");
        bank += 300;
        bankObj.GetComponentInChildren<TextMesh>().text = bank.ToString();
    }


    private void TurnOffSelectionHighlights() {
        for (var i = 0; i < shopButtons.Length; i++) shopButtons[i].SetActive(false);
    }

    private void Update() {
        AttemptSelection();
    }

    private GameObject ReturnClickedObject(out RaycastHit hit) {
        GameObject target = null;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction * 100, out hit)) target = hit.collider.gameObject;

        return target;
    }

    private void AttemptSelection() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hitInfo;
            target = ReturnClickedObject(out hitInfo);
            if (target != null) {
                if (target.transform.Find("itemText")) {
                    TurnOffSelectionHighlights();
                    Select();
                    UpdateDescriptionBox();

                    //NOT ALREADY SOLD
                    if (target.transform.Find("itemText").GetComponent<TextMesh>().text != "SOLD") {
                        //can afford
                        Affordable();
                        //can not afford
                        LackOfCredits();
                    }
                    else if (target.transform.Find("itemText").GetComponent<TextMesh>().text == "SOLD") {
                        SoldOut();
                    }
                }
                else if (target.name == "BUY ?") {
                    BuyItem();
                }
                else if (target.name == "START") {
                    StartGame();
                }
                else if (target.name == "WATCH AD") {
                    WatchAdvert();
                }
            }
        }
    }


    private void StartGame() {
        if (purchaseMade) {
            playerShip.name = "UpgradedShip";
            if (playerShip.transform.Find("energy +1(Clone)")) playerShip.GetComponent<Player>().Health = 2;

            DontDestroyOnLoad(playerShip);
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("testLevel");
    }

    private void BuyItem() {
        Debug.Log("PURCHASED");
        purchaseMade = true;
        buyButton.SetActive(false);
        tmpSelection.SetActive(false);

        for (var i = 0; i < visualWeapons.Length; i++)
            if (visualWeapons[i].name ==
                tmpSelection.transform.parent.gameObject.GetComponent<ShopPiece>().ShopSelection.iconName)
                visualWeapons[i].SetActive(true);

        UpgradeToShip(tmpSelection.transform.parent.gameObject.GetComponent<ShopPiece>().ShopSelection.iconName);
        bank = bank - int.Parse(tmpSelection.transform.parent.GetComponent<ShopPiece>().ShopSelection.cost);
        bankObj.transform.Find("bankText").GetComponent<TextMesh>().text = bank.ToString();
        tmpSelection.transform.parent.transform.Find("itemText").GetComponent<TextMesh>().text = "SOLD";
    }

    private void UpgradeToShip(string upgrade) {
        var shipItem = Instantiate(Resources.Load(upgrade)) as GameObject;
        shipItem.transform.SetParent(playerShip.transform);
        shipItem.transform.localPosition = Vector3.zero;
    }

    private void Select() {
        tmpSelection = target.transform.Find("SelectionQuad").gameObject;
        tmpSelection.SetActive(true);
    }

    private void UpdateDescriptionBox() {
        textBoxPanel.transform.Find("name").gameObject.GetComponent<TextMesh>().text =
            tmpSelection.GetComponentInParent<ShopPiece>().ShopSelection.iconName;
        textBoxPanel.transform.Find("desc").gameObject.GetComponent<TextMesh>().text =
            tmpSelection.GetComponentInParent<ShopPiece>().ShopSelection.description;
    }


    private void Affordable() {
        if (bank >= int.Parse(target.transform.GetComponent<ShopPiece>().ShopSelection.cost)) {
            Debug.Log("CAN BUY");
            buyButton.SetActive(true);
        }
    }

    private void LackOfCredits() {
        if (bank < int.Parse(target.transform.Find("itemText").GetComponent<TextMesh>().text)) Debug.Log("CAN'T BUY");
    }

    private void SoldOut() {
        Debug.Log("SOLD OUT");
    }

    private void TurnOffPlayerShipVisuals() {
        for (var i = 0; i < visualWeapons.Length; i++) visualWeapons[i].gameObject.SetActive(false);
    }

    private void PreparePlayerShipForUpgrade() {
        playerShip = Instantiate(defaultPlayerShip.actor);
        playerShip.GetComponent<Player>().enabled = false;
        playerShip.transform.position = new Vector3(0, 10000, 0);
        playerShip.GetComponent<IActorTemplate>().ActorStats(defaultPlayerShip);
    }
}