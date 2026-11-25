using UnityEngine;

public class ChaseState : IState
{
    private readonly EnemyController ctx;
    private float lostTimer;
    private float retreatUntil;

    public ChaseState(EnemyController ctx) => this.ctx = ctx;

    public void Enter() => lostTimer = 0f;

    public void Tick()
    {
        if (Time.time < retreatUntil)
        {
            if (!ctx.AtWall() && !(ctx.IsGrounded && ctx.AtEdge()))
                ctx.Move(ctx.Stats.patrolSpeed);
            else
                ctx.Stop();

            return;
        }

        float d = ctx.DistanceToTarget;
        bool atEdge = ctx.IsGrounded && ctx.AtEdge();

        bool lost = d > ctx.Stats.chaseRange * ctx.Stats.chaseExitFactor
                    || !ctx.TargetOnSameLevel
                    || atEdge;

        if (lost)
        {
            lostTimer += Time.deltaTime;

            if (lostTimer >= ctx.Stats.loseTargetTime)
            {
                if (atEdge)
                {
                    ctx.Flip();
                    retreatUntil = Time.time + 2f;
                }

                ctx.Machine.ChangeState(ctx.PatrolState);
                return;
            }
        }
        else
        {
            lostTimer = 0f;
        }

        ctx.FaceTarget();

        float safeStop = Mathf.Max(
            ctx.Stats.stopDistance,
            Mathf.Min(0.85f, ctx.Stats.attackRange * 0.9f)
        );

        bool canAdvance = d > safeStop
                          && !ctx.AtWall()
                          && !atEdge;

        if (canAdvance) ctx.Move(ctx.Stats.chaseSpeed);
        else            ctx.Stop();

        if (d <= ctx.Stats.attackRange && ctx.TargetOnSameLevel)
            ctx.Machine.ChangeState(ctx.AttackState);
    }

    public void Exit() => ctx.Stop();
}