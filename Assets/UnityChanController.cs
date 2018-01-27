using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour {

    //アニメーションするためのコンポーメントを入れる
    Animator animator;
    //地面の位置
    private float groundLevel = -3.0f;
    //Unityちゃんを移動させるコンポーメントを入れる
    Rigidbody2D rigid2d;
    //ジャンプの速度の減衰
    private float dump = 0.8f;
    //ジャンプの速度
    float jumpVelocity = 20f;
    //ゲームオーバーになる位置
    private float deadLine = -9;
	// Use this for initialization
	void Start () {
        //アニメーターのコンポーメントを取得する
        this.animator = GetComponent<Animator>();
        //Rigidbody2Dのコンポーメントを取得する
        this.rigid2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        //走るアニメーションを再生するために、Animotorのパラメータを調節する
        this.animator.SetFloat("Horizontal", 1);

        //着地しているかどうかを調べる
        bool isGround = (transform.position.y > this.groundLevel) ? false : true;
        this.animator.SetBool("isGround", isGround);

        //ジャンプ状態のときにはボリュームを0にする
        GetComponent<AudioSource>().volume = (isGround) ? 1 : 0;
        //着地状態でクリックされた場合
        if (Input.GetMouseButtonDown(0) && isGround)
        {
            //上方向の力をかける
            this.rigid2d.velocity = new Vector2(0, this.jumpVelocity);
        }
        //クリックをやめたら上方向への速度を減速する
        if (Input.GetMouseButton(0) == false)
        {
            if (this.rigid2d.velocity.y > 0)
            {
                this.rigid2d.velocity *= this.dump;
            }
        }
        //デッドラインを超えた場合ゲームオーバーになる
        if (transform.position.x < this.deadLine)
        {
            //UIControllerのGameOver関数を呼び出して画面上に「GameOver」と表示する
            GameObject.Find("Canvas").GetComponent<UIController>().GameOver();
            //ユニティちゃんを破棄する
            Destroy(gameObject);
        }
	}
}
