
package com.apitel_bank.main_banking_service.singletons;

import org.springframework.stereotype.Component;
import java.time.Instant;
import java.time.Duration;
import java.time.ZoneId;
import java.time.ZonedDateTime;
import java.util.concurrent.atomic.AtomicBoolean;

@Component
public class GameState {
    private Instant startTime;
    private ZonedDateTime gameStartDateTime;
    private final AtomicBoolean running = new AtomicBoolean(false);
    private static final long REAL_WORLD_MINUTES_PER_GAME_DAY = 2;
    private static final long MILLIS_PER_MINUTE = 60000;
    private static final long GAME_DAYS_PER_MONTH = 30;

    public void startGame(ZonedDateTime gameStartDate) {
        this.startTime = Instant.now();
        this.gameStartDateTime = gameStartDate;
        running.set(true);
    }

    public void resetGame(ZonedDateTime gameStartDate) {
        this.startTime = Instant.now();
        this.gameStartDateTime = gameStartDate;
    }

    public ZonedDateTime getCurrentGameTime() {
        if (running.get()) {
            long elapsedMillis = Duration.between(startTime, Instant.now()).toMillis();
            long gameDaysElapsed = elapsedMillis / (REAL_WORLD_MINUTES_PER_GAME_DAY * MILLIS_PER_MINUTE);
            return gameStartDateTime.plusDays(gameDaysElapsed);
        } else {
            return gameStartDateTime;
        }
    }
}
